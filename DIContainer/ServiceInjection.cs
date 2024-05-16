using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using System.Reflection;
using NewSampleAPI.Application.Interface;
using Application.FeatureCalculation.Repos.Command;
using NewSampleAPI.Service;

namespace Application.Services.DIContainer
{
	public static class ServiceInjection
	{
		public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration configuration)
		{
            services.AddAutoMapper(Assembly.GetExecutingAssembly());


			var awsConfigOptions = configuration.GetAWSOptions();// Add Config options
            services.AddDefaultAWSOptions(awsConfigOptions);

			services.AddAWSService<IAmazonDynamoDB>(); //Add DynmoDb services

			services.AddScoped<IDynamoDBContext, DynamoDBContext>(); //Add DynamoDb Context


			services.AddScoped<ICalcRespos,AddCalc>();

			services.AddScoped<ICalcServices, CalcServices>();


            return services;

        }
	}
}

