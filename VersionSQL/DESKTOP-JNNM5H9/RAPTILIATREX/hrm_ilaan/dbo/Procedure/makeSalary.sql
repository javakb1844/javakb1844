/****** Object:  Procedure [dbo].[makeSalary]    Committed by VersionSQL https://www.versionsql.com ******/

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[makeSalary] 
	-- Add the parameters for the stored procedure here
	@MonthNumber bigint,
	@YearNumber bigint,
	@EmployeeId bigint,
	@BasicSalary bigint,
    @CreatedBy bigint,

	@WorkingDaysRequired bigint,
    @PublicHolidays bigint,
    @Leaves bigint,
    @Absents bigint,
    @Attendance bigint,
    @TotalHoursRequired bigint,
    @TotalHours bigint,
	@SalaryParameter int,
	@Addition int,
	@Deduction int
	
	--@EmployeeId int,
	--@BasicSalaryinput decimal(18,2),
	--@IsTotalOnly int
AS
BEGIN



DECLARE	@BasicSalaryAnnual int
DECLARE	@PayRollId int


--set @MonthNumber=12
--set @YearNumber=2017
--set @EmployeeId=1
--set @CreatedBy=1
-- set @BasicSalary=50000

 set @BasicSalaryAnnual=@BasicSalary*12;
 
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	--select top 1  (fixtax+((760000-StartLimit)*TaxPercentageToMaxLimit/100)/12) as IncomeTax from Tax where StartLimit <760000.00 and MaxLimit >= 760000.00 order by TaxId desc;		
	
	
	BEGIN
	INSERT INTO [dbo].[PayRoll]
           ([EmployeeId]
           ,[MonthNumber]
           ,[YearNumber]
           ,[CreateDate]
           ,[CreatedBy]
           ,[GrossPay]
           ,[NetPay]
           ,[IncomeTax]
		   ,[WorkingDaysRequired]
           ,[PublicHolidays]
           ,[Leaves]
		   ,[Absents]
           ,[Attendance]
		   ,[TotalHoursRequired]
		   ,[TotalHours]
		   )
  
	select @EmployeeId as EmployeeId, @MonthNumber as MonthNumber, @YearNumber as YearNumber,getdate() as CreateDate,@CreatedBy as CreatedBy,isnull(Sum(case LookPolicy.ValueType when @Addition then (case LookPolicy.IsPercentage when 1 then   @BasicSalary * EmployeePolicy.PolicyValue/100 else   EmployeePolicy.PolicyValue end) end),0)  as GrossPay,
	        isnull(Sum(case LookPolicy.ValueType when @Addition then (case LookPolicy.IsPercentage when 1 then   @BasicSalary * EmployeePolicy.PolicyValue/100 else   EmployeePolicy.PolicyValue end) end),0)-isnull(sum(case LookPolicy.ValueType when @Deduction then (case LookPolicy.IsPercentage when 1 then   @BasicSalary * EmployeePolicy.PolicyValue/100 else   EmployeePolicy.PolicyValue end) end),0) as NetPay,
				  (select top 1  ((fixtax+((@BasicSalaryAnnual-StartLimit)*TaxPercentageToMaxLimit/100))/12) as IncomeTax from Tax where StartLimit <@BasicSalaryAnnual and MaxLimit >= @BasicSalaryAnnual order by TaxId desc) as IncomeTax,
			@WorkingDaysRequired as WorkingDaysRequired, @PublicHolidays as PublicHolidays,	@Leaves as Leaves,
			@Absents as Absents,  @Attendance as Attendance,  @TotalHoursRequired as TotalHoursRequired, @TotalHours as TotalHours		
					from EmployeePolicy
					inner join LookPolicy on  LookPolicy.LookPolicyId=EmployeePolicy.LookPolicyId
					where EmployeePolicy.LookPolicyGroupId=@SalaryParameter and EmployeePolicy.EmployeeId=@EmployeeId

					 SET @PayRollId =SCOPE_IDENTITY()
	
	END
		INSERT INTO PayRollDetail (PayRollId, EmployeePolicyId, LookPolicyId,PolicyName,PolicyUnit,LookPolicyGroupId,PolicyValue,ValueType,IsPercentage)
			select @PayRollId as PayRollId, EmployeePolicy.EmployeePolicyId,EmployeePolicy.LookPolicyId,EmployeePolicy.PolicyName,EmployeePolicy.PolicyUnit,EmployeePolicy.LookPolicyGroupId,EmployeePolicy.PolicyValue ,LookPolicy.ValueType,LookPolicy.IsPercentage
				from EmployeePolicy
				inner join LookPolicy on  LookPolicy.LookPolicyId=EmployeePolicy.LookPolicyId
				where EmployeePolicy.LookPolicyGroupId=@SalaryParameter and EmployeePolicy.EmployeeId=@EmployeeId
    -- Insert statements for procedure here
	
END
