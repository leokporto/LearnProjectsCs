// See https://aka.ms/new-console-template for more information

using EventSourcing.Console;
using EventSourcingDemo.Shared.Commands.Handlers;
using EventSourcingDemo.Staff.Domain.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Services.AddTransient<ICommandHandler<AddEmployeeCommand>, EmployeeCommandHandler>();

IHost host = builder.Build();
host.Run();