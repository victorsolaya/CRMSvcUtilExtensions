echo Generating the entities
echo ...
echo ...
CrmSvcUtil.exe ^
/codewriterfilter:"CRMSvcUtilExtensions.EntityFilter,CRMSvcUtilExtensions" ^
/url:https://environment.crmx.dynamics.com/XRMServices/2011/Organization.svc ^
/out:..\XrmContext.cs ^
/namespace:Namespace  ^
/username:username ^
/password:password ^
/serviceContextName:CrmServiceContext

echo Entities Generated

echo Generating Option Sets
echo ...
echo ...

CrmSvcUtil.exe ^
/codecustomization:"CRMSvcUtilExtensions.CodeCustomizationService,CRMSvcUtilExtensions" ^
/codewriterfilter:"CRMSvcUtilExtensions.FilteringService,CRMSvcUtilExtensions" ^
/url:https://environment.crmx.dynamics.com/XRMServices/2011/Organization.svc ^
/namingservice:"CRMSvcUtilExtensions.NamingService, CRMSvcUtilExtensions" ^
/username:username ^
/password:password ^
/namespace:Namespace ^
/out:..\XrmOptionSets.cs

echo OptionSets Generated

pause