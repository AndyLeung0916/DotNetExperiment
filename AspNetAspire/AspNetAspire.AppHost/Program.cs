using Custom.Hosting;// ��Aspire����ʱ���IsAspireProjectResource="false", �Ա�ע�����Ƿ�����

var builder = DistributedApplication.CreateBuilder(args);

//������Դ, �������ļ�appsettings.json��secret.json��ȡ
var username = builder.AddParameter("custom-username");
var password = builder.AddParameter("custom-password");

var customResource = builder.AddCustom("customresource", userName: username, password: password);// ����Զ���������Դ

builder.AddProject<Projects.Custom_Api>("customapi")// ���ӷ���
        .WithReference(customResource)// ����������Դ
        .WithCommand// �ڲ�������Actions������һ���Զ�����Դ����
        (
            name: "clear-cache",// �������������
            displayName: "Clear Cache",// �����������ʾ�����������
            executeCommand: context => Task.FromResult(CommandResults.Success()),// ��������ʱ����
            updateState: context => ResourceCommandState.Enabled,// ���ûص���ȷ�������"����"״̬
            iconName: "AnimalRabbitOff",// �����������ʾ��ͼ�������
            iconVariant: IconVariant.Filled// �����������ʾ��ͼ�����ʽ
        );
//.WaitFor(customResource);

// ������Դ, �������ļ�appsettings.json��secret.json��ȡ
var goVersion = builder.AddParameter("goversion");
var container1 = builder.AddDockerfile("mygoapp", "dockerfiles/1")// ��Dockerfile��ӵ���Դ, ���·��Ϊ��ǰ.csproj��·��
                        .WithBuildArg("GO_VERSION", goVersion)// ������Դ������ִ�а汾, ����ʱָ��, ��ӦDockerfile�е�ARG����; Ҳ��ֱ�Ӵ��ַ���ָ���汾
                        .WithBindMount("VolumeMount.AppHost-sql-data", "/var/opt/mssql");// �������ݾ�, ��һ������Ϊ���ش洢λ��, �ڶ�������Ϊ������·��
//.WithDockerfile("path");// ͨ��Dockerfile�Զ�������������Դ, ���·��Ϊ��ǰ.csproj��·��
//.WithBuildSecret("ACCESS_TOKEN", accessToken);// ָ��������Կ

var connectionString = builder.AddConnectionString("mysql");// ���������ַ���, �������ļ�������

builder.Build().Run();

/*
Aspire�����嵥����:
dotnet run --project AspNetAspire.AppHost/AspNetAspire.AppHost.csproj -- --publisher manifest --output-path aspire-manifest.json
*/