using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using DTO.Model.DTOCommon;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NLog.Extensions.Logging;
using NLog.Web;
using Sparkle_Framework2019.Controllers.Base;
using Swashbuckle.AspNetCore.Swagger;

namespace Sparkle_Framework2019
{
    /// <summary>
    /// 启动类
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        /// <summary>
        /// 配置类
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="services"></param>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            //获取IP
            services.AddSingleton<IHttpContextAccessor,HttpContextAccessor>();
            #region swagger和JWT
            //添加swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "通用框架2019",
                    Version = "V1 ",
                    Description = "Sparkle831143",
                    Contact = new Contact { Name = "Sparkle：", Url = "/views/test.html", Email = "a4200322@live.com" }
                });
                // 添加JWT（Bearer 输入框）
                var security = new Dictionary<string, IEnumerable<string>> { { "Sparkle_Framework2019", new string[] { } }, }; // 校验方案
                c.AddSecurityRequirement(security);
                c.AddSecurityDefinition("Sparkle_Framework2019", new ApiKeyScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer {token}（注意两者之间是一个空格）\"",
                    Name = "Authorization", //jwt默认的参数名称
                    In = "header",  //jwt默认存放Authorization信息的位置(请求头中)
                    Type = "apiKey"
                });
                //添加Swagger xml说明文档
                var basepath = Path.GetDirectoryName(typeof(Program).Assembly.Location);
                var xmlpath = Path.Combine(basepath, "Sparkle_Framework2019.xml");
                c.IncludeXmlComments(xmlpath);
            });
            #region JWT
            //认证
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    // 是否开启签名认证
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Token.SecurityKey)),
                    // 发行人验证，这里要和token类中Claim类型的发行人保持一致
                    ValidateIssuer = true,
                    ValidIssuer = "Sparkle",//发行人
                    // 接收人验证
                    ValidateAudience = true,
                    ValidAudience = "User",//订阅人
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                };
            });
            #endregion
            #endregion

            #region 跨域
            //添加跨域访问
            services.AddCors(options =>
            {
                options.AddPolicy("Cors", builder => builder.AllowAnyOrigin() //添加跨域访问规则
                 .AllowAnyMethod()
                 .AllowAnyHeader()
                 .AllowCredentials());
            });
            #endregion

            #region 实体映射
            //实体映射
            ColumnMapper.SetMapper();
            #endregion

            #region 获取token
            //获取请求头
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            #endregion

            #region 依赖注入
            var IOCbuilder = new ContainerBuilder();//建立容器
            List<Assembly> programlist = new List<Assembly> {Assembly.Load("Common"), Assembly.Load("DAL") };//批量反射程序集
            foreach (var q in programlist)
            {
                IOCbuilder.RegisterAssemblyTypes(q).AsImplementedInterfaces(); //注册容器
            }
            IOCbuilder.Populate(services);//将service注入到容器
            var ApplicationContainner = IOCbuilder.Build();//登记创建容器

            return new AutofacServiceProvider(ApplicationContainner);   //IOC接管
            #endregion

        }

        /// <summary>
        /// 中间件
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory">日志工厂</param>
        [Obsolete]
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            //Nlog日志
            loggerFactory.AddNLog();
            env.ConfigureNLog("nlog.config");

            //重定向
            app.UseHttpsRedirection();
            //访问静态文件
            app.UseStaticFiles();

            //Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "sparkle");
            });
            // JWT验证
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
