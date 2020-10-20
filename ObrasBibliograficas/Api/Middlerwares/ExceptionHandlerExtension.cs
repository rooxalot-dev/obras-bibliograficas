﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Api.Middlerwares
{
	public static class ExceptionHandlerExtension
	{
		public static void UseGlobalExceptionHandler(this IApplicationBuilder app, ILoggerFactory loggerFactory)
		{
			app.UseExceptionHandler(builder =>
			{
				builder.Run(async context =>
				{
					var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();

					if (exceptionHandlerFeature != null)
					{
						var logger = loggerFactory.CreateLogger("GlobalExceptionHandler");
						logger.LogError($"Unexpected error: {exceptionHandlerFeature.Error}");

						context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
						context.Response.ContentType = "application/json";

						var json = new
						{
							context.Response.StatusCode,
							Message = "Ocorreu um erro ao processar a requisição",
							Detailed = exceptionHandlerFeature.Error.Message
						};

						await context.Response.WriteAsync(JsonConvert.SerializeObject(json));
					}
				});
			});
		}
	}
}
