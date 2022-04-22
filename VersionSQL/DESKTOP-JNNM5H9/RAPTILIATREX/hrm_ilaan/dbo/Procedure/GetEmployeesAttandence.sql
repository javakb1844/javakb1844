/****** Object:  Procedure [dbo].[GetEmployeesAttandence]    Committed by VersionSQL https://www.versionsql.com ******/

-- =============================================
-- Author:		<Author,,Wasim 05-01-2022>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetEmployeesAttandence]
	-- Add the parameters for the stored procedure here
	 @DateFrom date=null,
	 @DateTo  date=null,
	 @monthNo int,
	 @biometricid int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	declare @WhereMain varchar(500)='';
	declare @Where varchar(500)='';
	declare @query varchar(5000);
	--if(@biometricid>0)
	--begin
	--set @WhereMain=' and  emp.biometricid='+cast(@biometricid as varchar(12));
	--end
	
	--if(@DateFrom!='')
	--begin	
	--set @Where=@Where+' and   dateonly>='+@DateFrom ;
	--end
	--if(@DateTo!='')
	--begin
	--set @Where=@Where+' and   dateonly<='+@DateTo ;
	--end
	
	--if(@monthNo>0)
	--begin
	--set @Where=@Where+' and   Month(dateonly)='+cast(@monthNo as varchar(20));
	--end
	
	--print @DateFrom;
    -- Insert statements for procedure here
	----set @query='select  intime.BiometricId,intime.DateOnly as InDateOnly,intime.TimeIn,
 ---- outtime.dateonly as OutDateOnly,outtime.TimeOut,  datediff(minute,intime.timein, outtime.TimeOut) as TotalMinutes,datename(dw,intime.DateOnly) as DayCaption
 ---- from 
 ---- [hrm_ilaan].[dbo].[Employee] emp left join   
 ---- (
 ---- select min(datetimerecord) as timein,dateonly,biometricid
 ---- from [hrm_ilaan].[dbo].[BioMetricData] 
 ---- where 1=1 '+@Where+'
 ---- group by dateonly,biometricid
 ---- ) intime    on emp.[BioMetricId]=intime.biometricid  left join  

 ----  ( select max(datetimerecord) as TimeOut,dateonly,biometricid
 ---- from [hrm_ilaan].[dbo].[BioMetricData]
 ---- where  1=1 '+@Where+'
 ---- group by dateonly,biometricid) outtime on emp.[BioMetricId]=outtime.biometricid and intime.dateonly=outtime.dateonly
 ---- where 1=1 '+@WhereMain+' order by intime.DateOnly' ;
   
--   declare @startdate date = @DateFrom;
--declare @enddate date = @DateTo;


with
  dates as (
    select @DateFrom as [date]
    union all
    select dateadd(dd, 1, [date]) from dates where [date] < @DateTo
  )

select  emp.EmployeeId, dates.[date] as LogDate,emp.BiometricId,intime.DateOnly as InDateOnly,intime.TimeIn,
  outtime.dateonly as OutDateOnly,outtime.TimeOut,  datediff(minute,intime.timein, outtime.TimeOut) as TotalMinutes,datename(dw,dates.[date]) as DayCaption,hrpc.[PolicyValue]
  from 
  [hrm_ilaan].[dbo].[Employee] emp cross join dates left join   
  (
  select min(datetimerecord) as timein,dateonly,biometricid
  from [hrm_ilaan].[dbo].[BioMetricData] 
  where 1=1  and   dateonly>=@DateFrom and   dateonly<=@DateTo
  group by dateonly,biometricid
  ) intime    on emp.[BioMetricId]=intime.biometricid  and dates.[date]=intime.dateonly left join  

   ( select max(datetimerecord) as TimeOut,dateonly,biometricid
  from [hrm_ilaan].[dbo].[BioMetricData]
  where  1=1  and   dateonly>=@DateFrom and   dateonly<=@DateTo
  group by dateonly,biometricid) outtime on emp.[BioMetricId]=outtime.biometricid and dates.[date]=outtime.dateonly

  left join [hrm_ilaan].[dbo].[HRPolicyByCaption]  hrpc on emp.WorkingHourPolicyId=hrpc.[HRPolicyCaptionId] and hrpc.[PolicyName]=datename(dw,dates.[date])

  where 1=1  and  emp.biometricid=@biometricid order by dates.[date]




 --  exec(@query)	
  -- print @query;
END
