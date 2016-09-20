namespace Kevins.Examples.Common.Enums
{
    public enum DeliveryMethod
    {
        Email = 1000,
        Fax = 2000,
        Print = 3000,
        BatchPrint = 4000,
        ServerRequest = 5000,
        Undefined = 0
    }



    public enum DeliveryQueueType
    {
        PrepInstructions = 1000,
        ExamReport = 2000,
        ExamReportExtraCopy = 2001,
        ExamReportNursingUnit = 2002,
        ExamReportServerSidePrinting = 2003,
        ExamReportAdhoc = 2004,
        ExamReportExtraEmail = 2005,
        AppointmentConfirmation = 3000,
        AutoAppointmentConfirmation = 3001,
        NoteToReferenceDoctor = 4000,
        FollowupLetter = 5000,
        LayPersonLetter = 6000,
        MedicalDirector = 7000,
        ImpressionToReferenceDoctor = 8000,
        PeerReview = 9000,
        NewUserRequest = 10000,
        EmailNotice = 11000,
        Undefined = 0
    }



    public enum DeliveryStatus
    {
        ToBeSent = 1000,
        OnHold = 2000,
        Hidden = 3000,
        ToBeDeleted = 4000,
        Sent = 5000,
        PendingFax = 6000,
        Failed = 7000,
        PendingAuditPdfUpload = 9000
    }



    public enum InterpretationStatus
    {
        Final = 3,
        Preliminary = 2,
    }



    public enum OpinionType
    {
        Agree = 1,
        PartiallyAgree = 2,
        Disagree = 3,
        Undefined = 0
    }



    public enum Priority
    {
        Normal = 1000,
        SendNow = 2000,
        ServerPrint = 3000,
        BatchPrint = 4000,
        Top = 9000
    }
}
