/****** Object:  Procedure [dbo].[CalculateSalary]    Committed by VersionSQL https://www.versionsql.com ******/

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CalculateSalary]
	-- Add the parameters for the stored procedure here
	@EmployeeId int,
	@BasicSalary decimal(18,2),
	@IsTotalOnly int,
	@SalaryParameter int,
	@Addition int,
	@Deduction int
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	if(@IsTotalOnly>0)
		 BEGIN
		    if(@EmployeeId>0)
				BEGIN
		 			select sum(case LookPolicy.ValueType when @Deduction then (case LookPolicy.IsPercentage when 1 then   @BasicSalary * EmployeePolicy.PolicyValue/100 else   EmployeePolicy.PolicyValue end) end)  as Deduction, Sum(case LookPolicy.ValueType when @Addition then (case LookPolicy.IsPercentage when 1 then   @BasicSalary * EmployeePolicy.PolicyValue/100 else   EmployeePolicy.PolicyValue end) end)  as Amount
					from EmployeePolicy
					inner join LookPolicy on  LookPolicy.LookPolicyId=EmployeePolicy.LookPolicyId
					where EmployeePolicy.LookPolicyGroupId=@SalaryParameter and EmployeePolicy.EmployeeId=@EmployeeId
			   END
			else
			   BEGIN
		 			select sum(case LookPolicy.ValueType when @Deduction then (case LookPolicy.IsPercentage when 1 then   @BasicSalary * EmployeePolicy.PolicyValue/100 else   EmployeePolicy.PolicyValue end) end)  as Deduction, Sum(case LookPolicy.ValueType when @Addition then (case LookPolicy.IsPercentage when 1 then   @BasicSalary * EmployeePolicy.PolicyValue/100 else   EmployeePolicy.PolicyValue end) end)  as Amount
					from EmployeePolicy
					inner join LookPolicy on  LookPolicy.LookPolicyId=EmployeePolicy.LookPolicyId
					where EmployeePolicy.LookPolicyGroupId=@SalaryParameter
			   END
		 END
	 else
		BEGIN
		 if(@EmployeeId>0)
			 BEGIN
				select EmployeePolicy.* ,LookPolicy.ValueType,LookPolicy.IsPercentage, case LookPolicy.ValueType when @Deduction then (case LookPolicy.IsPercentage when 1 then   @BasicSalary * EmployeePolicy.PolicyValue/100 else   EmployeePolicy.PolicyValue end) else 0  end  as Deduction,  case LookPolicy.ValueType when @Addition then (case LookPolicy.IsPercentage when 1 then   @BasicSalary * EmployeePolicy.PolicyValue/100 else   EmployeePolicy.PolicyValue end) else 0  end  as Amount
				from EmployeePolicy
				inner join LookPolicy on  LookPolicy.LookPolicyId=EmployeePolicy.LookPolicyId
				where EmployeePolicy.LookPolicyGroupId=@SalaryParameter and EmployeePolicy.EmployeeId=@EmployeeId
			END
			else
			 BEGIN
				select EmployeePolicy.* ,LookPolicy.ValueType,LookPolicy.IsPercentage, case LookPolicy.ValueType when @Deduction then (case LookPolicy.IsPercentage when 1 then   @BasicSalary * EmployeePolicy.PolicyValue/100 else   EmployeePolicy.PolicyValue end) else 0  end  as Deduction,  case LookPolicy.ValueType when @Addition then (case LookPolicy.IsPercentage when 1 then   @BasicSalary * EmployeePolicy.PolicyValue/100 else   EmployeePolicy.PolicyValue end) else 0  end  as Amount
				from EmployeePolicy
				inner join LookPolicy on  LookPolicy.LookPolicyId=EmployeePolicy.LookPolicyId
				where EmployeePolicy.LookPolicyGroupId=@SalaryParameter 
			END
	    END
		

   
	
END
