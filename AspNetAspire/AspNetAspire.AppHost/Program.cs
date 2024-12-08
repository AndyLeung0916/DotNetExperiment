using Custom.Hosting;// ��Aspire����ʱ���IsAspireProjectResource="false", �Ա�ע�����Ƿ�����

var builder = DistributedApplication.CreateBuilder(args);

//������Դ, �������ļ�appsettings.json��secret.json��ȡ
var username = builder.AddParameter("custom-username");
var password = builder.AddParameter("custom-password");

var customResource = builder.AddCustom("customresource", userName: username, password: password);// ����Զ���������Դ

builder.AddProject<Projects.Custom_Api>("customapi")// ���ӷ���
        .WithReference(customResource);// ����������Դ
                                       //.WaitFor(customResource);

builder.Build().Run();

/*
 Aspire�����嵥����:
dotnet run --project AspNetAspire.AppHost/AspNetAspire.AppHost.csproj -- --publisher manifest --output-path aspire-manifest.json
 */