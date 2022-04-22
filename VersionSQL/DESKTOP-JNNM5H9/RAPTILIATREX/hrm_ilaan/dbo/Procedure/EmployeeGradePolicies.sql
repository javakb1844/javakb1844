/****** Object:  Procedure [dbo].[EmployeeGradePolicies]    Committed by VersionSQL https://www.versionsql.com ******/

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[EmployeeGradePolicies] 
	-- Add the parameters for the stored procedure here
   @LookDesignationId int, 
	@LookPolicyGroupId int, 
	@EmployeeId int,
	@IsEmployeeOnly bit  
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	if(@LookPolicyGroupId>0)
		 BEGIN
		  if(@IsEmployeeOnly=0)
			  BEGIN
			  select LookPolicy.*,HRPolicy.PolicyValue,EmployeePolicy.EmployeePolicyId,EmployeePolicy.PolicyValue as EmployeePolicyValue from LookPolicy 
				 inner join HRPolicy on HRPolicy.LookPolicyId=LookPolicy.LookPolicyId
				 left join EmployeePolicy on EmployeePolicy.LookPolicyId= LookPolicy.LookPolicyId and EmployeePolicy.EmployeeId =@EmployeeId
				where HRPolicy.GroupTypeId=2 and  LookPolicy.LookPolicyGroupId=@LookPolicyGroupId  and HRPolicy.LookDesignationId=@LookDesignationId 
			   END
           else
			 BEGIN
		  --select LookPolicy.*,HRPolicy.PolicyValue,EmployeePolicy.EmployeePolicyId,EmployeePolicy.PolicyValue as EmployeePolicyValue from LookPolicy 
			 --inner join HRPolicy on HRPolicy.LookPolicyId=LookPolicy.LookPolicyId
			 --left join EmployeePolicy on EmployeePolicy.LookPolicyId= LookPolicy.LookPolicyId and EmployeePolicy.EmployeeId =@EmployeeId
    --        where LookPolicy.LookPolicyGroupId=@LookPolicyGroupId  and HRPolicy.LookDesignationId=@LookDesignationId  and EmployeePolicy.EmployeeId =@EmployeeId
		  
		    select EmployeePolicy.LookPolicyId,EmployeePolicy.EmployeePolicyId,EmployeePolicy.PolicyName,EmployeePolicy.PolicyUnit,
			 EmployeePolicy.LookPolicyGroupId,HRPolicy.PolicyValue,   EmployeePolicy.PolicyValue as EmployeePolicyValue
			 from EmployeePolicy 
			 inner join employee on Employee.EmployeeId=EmployeePolicy.EmployeeId
			 inner join HRPolicy on Employee.LookGradeId=HRPolicy.LookDesignationId and EmployeePolicy.LookPolicyId=HRPolicy.LookPolicyId
             where  HRPolicy.GroupTypeId=2 and   EmployeePolicy.LookPolicyGroupId=@LookPolicyGroupId  and EmployeePolicy.EmployeeId =@EmployeeId ORDER BY LookPolicyId

		   END

		END
		else
		 BEGIN
		    if(@IsEmployeeOnly=0)
			BEGIN
			 select LookPolicy.*,HRPolicy.PolicyValue,EmployeePolicy.EmployeePolicyId,EmployeePolicy.PolicyValue as EmployeePolicyValue from LookPolicy 
			 inner join HRPolicy on HRPolicy.LookPolicyId=LookPolicy.LookPolicyId
			 left join EmployeePolicy on EmployeePolicy.LookPolicyId= LookPolicy.LookPolicyId and EmployeePolicy.EmployeeId =@EmployeeId
            where HRPolicy.GroupTypeId=2 and   HRPolicy.LookDesignationId= @LookDesignationId 
			ORDER BY LookPolicyId
		   END
		   else
		   BEGIN

		    select EmployeePolicy.LookPolicyId,EmployeePolicy.PolicyName,EmployeePolicy.PolicyUnit,
			 EmployeePolicy.LookPolicyGroupId,HRPolicy.PolicyValue, EmployeePolicy.EmployeePolicyId,  EmployeePolicy.PolicyValue as EmployeePolicyValue
			 from EmployeePolicy 
			 inner join employee on Employee.EmployeeId=EmployeePolicy.EmployeeId
			 inner join HRPolicy on Employee.LookGradeId=HRPolicy.LookDesignationId and EmployeePolicy.LookPolicyId=HRPolicy.LookPolicyId
             where HRPolicy.GroupTypeId=2 and   EmployeePolicy.EmployeeId =@EmployeeId ORDER BY LookPolicyId

			 --select LookPolicy.*,HRPolicy.PolicyValue,EmployeePolicy.EmployeePolicyId,EmployeePolicy.PolicyValue as EmployeePolicyValue from LookPolicy 
			 --inner join HRPolicy on HRPolicy.LookPolicyId=LookPolicy.LookPolicyId
			 --left join EmployeePolicy on EmployeePolicy.LookPolicyId= LookPolicy.LookPolicyId and EmployeePolicy.EmployeeId =@EmployeeId
    --        where HRPolicy.LookDesignationId=@LookDesignationId and EmployeePolicy.EmployeeId =@EmployeeId
		   END
		END


			
    -- Insert statements for procedure here
	
END
