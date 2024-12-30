namespace ElderlyCareSupport.Server.Common
{
    public static class CommonConstants
    {

        // Response Helper Constants

        public const string StatusMessageOk = "OK";
        public const string StatusMessageNotFound = "Not Found";
        public const string StatusMessageBadRequest = "Bad Request";
        public const string ValidationErrorMessage = "Validation Error Occurred";
        public const string OperationFailedErrorMessage = "{0} Process has been failed.";
        public const string NotFound = "{0} Not Found";
        public const string UserAlreadyExisted = "The user is already existed";


        // Email Helper Constants

        public const string SenderEmailAddress = "guhan000714@gmail.com";
        public const string SenderNamePlaceHolder = "Sender Name";
        public const string EmailSubject = "ElderlyCareSupport Regsitration Mail";
        public const string RegistrationMailContentPath = @"D:\Qantler\Learnings\Elderly_Care_Support_V1\ElderlyCareSupport.Server\EmailTemplateView\RegistrationEmailBody.html";
    }
}
