namespace ElderlyCareSupport.Application.Common
{
    public static class Constants
    {

        // Response Helper Constants

        public static readonly string StatusMessageOk = "OK";
        public static readonly string StatusMessageNotFound = "Not Found";
        public static readonly string StatusMessageBadRequest = "Bad Request";
        public static readonly string ValidationErrorMessage = "Validation Error Occurred";
        public static readonly string OperationFailedErrorMessage = "{0} Process has been failed.";
        public static readonly string NotFound = "{0} Not Found";
        public static readonly string UserAlreadyExisted = "The user is already existed";


        // Email Helper Constants

        public static readonly string SenderEmailAddress = "guhan000714@gmail.com";
        public static readonly string SenderNamePlaceHolder = "Sender Name";
        public static readonly string EmailSubject = "ElderlyCareSupport Registration Mail";
        public static readonly string RegistrationMailContentPath = @"D:\Qantler\Learnings\Elderly_Care_Support_V1\ElderlyCareSupport.WebApi\EmailTemplateView\RegistrationEmailBody.html";
    }
}
