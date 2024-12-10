using Microsoft.AspNetCore.Mvc;

namespace MyAspNet;

// �����Ե�Asp.Net��ĿProgram���Ϊpartial, �Ա���Ա����ɲ�����Ŀ����
public partial class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        app.MapGet("/Add", ([FromQuery] int i, [FromQuery] int j) => i + j);
        app.MapGet("/", () => "Hello World!");

        app.Run();
    }
}
