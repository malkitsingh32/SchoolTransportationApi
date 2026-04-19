using Application.Abstraction.Repositories;
using Application.Abstraction.Services;
using Application.Common.Helpers;
using Application.Common.Interfaces.Common;
using Application.ExternalAPI;
using DTO.Request.CardknoxPaymentMethod;
using Infrastructure.Implementation.Common;
using Infrastructure.Implementation.Repositories;
using Infrastructure.Implementation.Services;
using Infrastructure.Implementation.Services.Email;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            #region Service
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRequestBuilder, RequestBuilder>();
            services.AddTransient<IAdminService, AdminService>();
            services.AddTransient<IStudentsService, StudentsService>();
            services.AddTransient<IBusDetailsService, BusDetailsService>();
            services.AddTransient<IDriversService, DriversService>();
            services.AddTransient<IRoutesService, RoutesService>();
            services.AddTransient<IStreetsService, StreetsService>();
            services.AddTransient<IChargesService, ChargesService>();
            services.AddTransient<IPaymentsService, PaymentsService>();
            services.AddTransient<IBackgroundServices, BackgroundsServices>();
            services.AddTransient<IPayrollService, PayrollService>();
            services.AddTransient<INotificationService, NotificationService>();
            services.AddTransient<TwilioService>();
            services.AddTransient<ISendGridEmail, SendGridEmail>();
            services.AddTransient<IPdfBuilderService, PdfBuilderService>();
            services.AddTransient<ISmsService, SmsService>();
            services.AddTransient<ICardknoxService, CardknoxService>();
            services.AddTransient<ICardknoxMonthlyTaskService, CardknoxMonthlyTaskService>();
            services.AddTransient<IPaymentTransactionService, PaymentTransactionService>();
            services.AddTransient<IBillingService, BillingService>();
            services.AddTransient<IPaymentHistoryService, PaymentHistoryService>();
            services.AddTransient<IBusChangesService, BusChangesService>();
            #endregion

            #region Repository
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IAdminRepository, AdminRepository>();
            services.AddTransient<IBusDetailsRepository, BusDetailsRepository>();
            services.AddTransient<IStudentsRepository, StudentsRepository>();
            services.AddTransient<IDriversRepository, DriversRepository>();
            services.AddTransient<IRoutesRepository, RoutesRepository>();
            services.AddTransient<IStreetsRepository, StreetsRepository>();
            services.AddTransient<IChargesRepository, ChargesRepository>();
            services.AddTransient<IPaymentsRepository, PaymentsRepository>();
            services.AddTransient<IPayrollRepository, PayrollRepository>();
            services.AddTransient<IBackgroundRepository, BackgroundRepository>();
            services.AddTransient<INotificationRepository, NotificationRepository>();
            services.AddTransient<ICardknoxRepository, CardknoxRepository>();
            services.AddTransient<IPaymentHistoryRepository, PaymentHistoryRepository>();
            services.AddTransient<IBusChangesRepository, BusChangesRepository>();
            services.AddTransient<Utility>();

            #endregion
        }

    }
}
