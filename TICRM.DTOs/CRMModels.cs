using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TICRM.DTOs.Base;

namespace TICRM.DTOs
{
    public class AccountDto : BaseDto
    {
        public System.Guid AccountId { get; set; }
        [Required]
        public string Name { get; set; }
        public Nullable<System.Guid> ShippingAddress { get; set; }
        public Nullable<System.Guid> BillingAddress { get; set; }
        [Required]
        public Nullable<System.Guid> AccountTypeId { get; set; }
        [Required]
        [MinLength(11)]
        [MaxLength(11)]
        public string PhoneOffice { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        public string Fax { get; set; }
        public string WebSite { get; set; }
        [Required]
        public Nullable<System.Guid> AccountSizeId { get; set; }
        [Required]
        public Nullable<System.Guid> IndustryId { get; set; }
        public string Description { get; set; }
        public virtual AccountSizeDto AccountSize { get; set; }
        public virtual AccountTypeDto AccountType { get; set; }
        public virtual AddressDto Address { get; set; }
        public virtual AddressDto Address1 { get; set; }

        public virtual IndustryDto Industry { get; set; }

    }
    public class OpportunityDto : BaseDto
    {
        public System.Guid OpportunityId { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<System.Guid> ProbabilityId { get; set; }
        public Nullable<System.Guid> OpportunityStageId { get; set; }
        public string Title { get; set; }
        public Nullable<System.Guid> CurrencyId { get; set; }
        public string Description { get; set; }
        public virtual CurrencyDto Currency { get; set; }

        public virtual OpportunityStageDto OpportunityStage { get; set; }
        public virtual PobabilityDto Pobability { get; set; }

        public Nullable<System.Guid> AccountId { get; set; }

    }
    public class LeadDto : BaseDto
    {
        public System.Guid LeadId { get; set; }
        [Required]
        public string Name { get; set; }
        public Nullable<System.Guid> LeadTypeId { get; set; }
        public Nullable<System.Guid> LeadSourceId { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public Nullable<System.Guid> AddressId { get; set; }
        public Nullable<System.Guid> IndustryId { get; set; }
        public string Description { get; set; }


        public virtual AddressDto Address { get; set; }
        public virtual IndustryDto Industry { get; set; }
        public virtual LeadSourceDto LeadSource { get; set; }
        public virtual LeadTypeDto LeadType { get; set; }

    }
    //added by zaman khan for contact entity
    public class ContactDto : BaseDto
    {
        public long ContactId { get; set; }
        public Nullable<System.Guid> AccountId { get; set; }

        [Required]
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public virtual AccountDto Account { get; set; }
    }
    public class CustomerAssetDto : BaseDto
    {
        public System.Guid CustomerAssetId { get; set; }
        public string Title { get; set; }
        public Nullable<System.Guid> CustomerAssetTypeId { get; set; }
        public string Manufacture { get; set; }
        public string Model { get; set; }
        public Nullable<int> YearOfManufacture { get; set; }
        public Nullable<decimal> Value { get; set; }
        public Nullable<decimal> DepriciatedValue { get; set; }
        public string SKU { get; set; }
        public string Description { get; set; }
        public virtual CustomerAssetTypeDto CustomerAssetType { get; set; }

        public Nullable<System.Guid> AccountId { get; set; }
        public Nullable<System.Guid> LocationId { get; set; }

    }
    public class LocationDto : BaseDto
    {
        public System.Guid LocationId { get; set; }
        public string Name { get; set; }
        public Nullable<System.Guid> AddressId { get; set; }
        public Nullable<System.Guid> LocationTypeId { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> Latitude { get; set; }
        public Nullable<decimal> Longitude { get; set; }
        public virtual LocationTypeDto LocationType { get; set; }
        public virtual AddressDto Address { get; set; }

        public Nullable<System.Guid> AccountId { get; set; }

    }

    public class AlertDto : BaseDto
    {
        public System.Guid AlertId { get; set; }
        public string Title { get; set; }
        public System.Guid UrgencyId { get; set; }
        public string Description { get; set; }
        public virtual UrgencyDto Urgency { get; set; }

    }

    public class DeviceDto : BaseDto
    {
        public System.Guid DeviceId { get; set; }

        [Required]
        [RegularExpression(@"^[^\s\,]+$", ErrorMessage = "Name Cannot Have Spaces")]
        public string Name { get; set; }
        public string Mac { get; set; }
        public string EMEINumber { get; set; }

        public string CloudServices { get; set; }
        public string CloudData { get; set; }

        public Nullable<System.DateTime> RegistrationDate { get; set; }
        public Nullable<System.DateTime> ServiceDate { get; set; }
        public Nullable<decimal> Latitude { get; set; }
        public Nullable<decimal> Longitude { get; set; }

        public Nullable<System.Guid> AccountId { get; set; }
        public Nullable<System.Guid> CustomerAssetId { get; set; }
        public string Maintenance { get; set; }

        public virtual ICollection<DeciveConfigurationDto> DeciveConfigurations { get; set; }
        public virtual ICollection<DeviceSensorDto> DeviceSensors { get; set; }
    }

    public class ReadingDto : BaseDto
    {
        public System.Guid ReadingId { get; set; }
        public Nullable<decimal> Value { get; set; }
        public Nullable<System.Guid> ReadingTypeId { get; set; }
        public Nullable<System.Guid> ReadingUnitId { get; set; }
        public Nullable<decimal> MarginOfErrorInPercent { get; set; }
        public string Description { get; set; }

        public virtual ReadingTypeDto ReadingType { get; set; }
        public virtual ReadingUnitDto ReadingUnit { get; set; }
    }

    public class ReadingUnitDto
    {
        public System.Guid ReadingUnitId { get; set; }
        public string Name { get; set; }
        public Nullable<System.Guid> Type { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public virtual ReadingTypeDto ReadingType { get; set; }
    }

    public class ServiceCallDto : BaseDto
    {
        public System.Guid ServiceCallId { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public Nullable<System.Guid> UrgencyId { get; set; }
        public Nullable<System.Guid> ServiceCallStageId { get; set; }
        public string Description { get; set; }

        public virtual UrgencyDto Urgency { get; set; }

        public virtual WorkStageDto WorkStage { get; set; }
    }

    public class CategoryDto : BaseDto
    {
        public System.Guid CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

    }

    public class ResourceDto : BaseDto
    {
        public System.Guid ResourceId { get; set; }
        public string Name { get; set; }
        public Nullable<System.Guid> Address { get; set; }
        public Nullable<System.Guid> CurrentAddress { get; set; }
        public string PhoneHome { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string PhoneOffice { get; set; }
        public string Description { get; set; }
        public virtual AddressDto Address1 { get; set; }
        public virtual AddressDto Address2 { get; set; }
    }

    public class ResourceAvailabilityDto : BaseDto
    {
        public System.Guid ResourceAvailabilityId { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<decimal> Hour { get; set; }
        public Nullable<System.DateTime> StartTime { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
        public string Description { get; set; }

    }

    public class ResourceSkillDto : BaseDto
    {
        public System.Guid ResourceSkillId { get; set; }
        public Nullable<System.Guid> SkillId { get; set; }
        public Nullable<System.Guid> SkillLevelId { get; set; }
        public string Description { get; set; }
        public virtual SkillLevelDto SkillLevel { get; set; }
        public virtual SkillDto Skill { get; set; }

    }

    public class WorkOrderDto : BaseDto
    {
        public System.Guid WorkOrderId { get; set; }
        public string Title { get; set; }
        public Nullable<decimal> NTE { get; set; }
        public Nullable<System.Guid> WorkOrderStageId { get; set; }
        public string Description { get; set; }
        public virtual WorkStageDto WorkStage { get; set; }
    }


    public class SkillLevelDto
    {


        public System.Guid SkillLevelId { get; set; }
        public string Name { get; set; }


    }

    public class SkillDto
    {
        public System.Guid SkillId { get; set; }
        public string Name { get; set; }
    }

    public partial class WorkStageDto
    {
        public System.Guid WorkStageId { get; set; }
        public string Name { get; set; }
    }


    public class ReadingTypeDto
    {
        public System.Guid ReadingTypeId { get; set; }
        public string Name { get; set; }
        public Nullable<bool> IsDeleted { get; set; }

    }

    public class DeviceSensorDto
    {


        public System.Guid DeviceSensorId { get; set; }
        public string SensorName { get; set; }
        public Nullable<System.Guid> DeviceId { get; set; }
        public string SensorType { get; set; }
        public Nullable<System.Guid> StatusId { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }

        public virtual DeviceDto Device { get; set; }
        public virtual StatusDto Status { get; set; }

        public virtual ICollection<SensorDataDto> SensorDatas { get; set; }
    }
    public class SensorDataDto
    {
        public long SensorDataId { get; set; }
        public Nullable<System.Guid> DeviceSensorId { get; set; }
        public Nullable<double> SensorValue { get; set; }
        public Nullable<System.DateTime> RecordDate { get; set; }

        public virtual DeviceSensorDto DeviceSensor { get; set; }
    }
    public class DeciveConfigurationDto
    {
        public System.Guid DeviceConfigurationId { get; set; }
        public Nullable<System.Guid> DeviceId { get; set; }
        public Nullable<bool> IsSleepModeEnabled { get; set; }
        public Nullable<int> SleepModeValueInSeconds { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }

        public virtual DeviceDto Device { get; set; }
    }
    public class UrgencyDto
    {
        public System.Guid UrgencyId { get; set; }
        public string Name { get; set; }
    }
    public class LocationTypeDto
    {
        public System.Guid LocationTypeId { get; set; }
        public string Name { get; set; }
    }
    public class CustomerAssetTypeDto
    {
        public System.Guid CustomerAssetTypeId { get; set; }
        public string Name { get; set; }
    }
    public class LeadTypeDto
    {

        public System.Guid LeadTypeId { get; set; }
        public string Name { get; set; }


    }
    public class LeadSourceDto
    {
        public System.Guid LeadSourceId { get; set; }
        public string Name { get; set; }


    }
    public class AccountLeadDto
    {
        public long Id { get; set; }
        public Nullable<long> AccountId { get; set; }
        public Nullable<long> LeadId { get; set; }
    }
    public class AccountTypeDto
    {
        public System.Guid AccountTypeId { get; set; }
        public string Name { get; set; }

    }
    public class AccountSizeDto
    {
        public System.Guid AccountSizeId { get; set; }
        public string Name { get; set; }
    }
    public class AddressDto
    {

        public System.Guid AddressId { get; set; }
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public Nullable<bool> IsDeleted { get; set; }

    }
    public class IndustryDto
    {
        public System.Guid IndustryId { get; set; }
        public string Name { get; set; }
    }
    public class StatusDto
    {
        public System.Guid StatusId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }
    public class TeamDto : BaseBasicDto
    {
        public System.Guid TeamId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

    }
    public class UserDto : BaseBasicDto
    {

        public System.Guid UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

    }
    public class CurrencyDto
    {
        public System.Guid CurrencyId { get; set; }
        public string Name { get; set; }
        public Nullable<decimal> Value { get; set; }
        public string Sign { get; set; }

    }
    public class OpportunityStageDto
    {
        public System.Guid OpportunityStageId { get; set; }
        public string Name { get; set; }

    }
    public class PobabilityDto
    {
        public System.Guid ProbabilityId { get; set; }
        public string Name { get; set; }
    }


    public class DeviceSensorGraphDto
    {
        public System.Guid DeviceSensorGraphId { get; set; }
        public Nullable<System.Guid> DeviceId { get; set; }
        public Nullable<System.Guid> SensorId { get; set; }
        public Nullable<System.Guid> GraphId { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    }

    public class GraphDto
    {
        public System.Guid GraphId { get; set; }
        public string GraphName { get; set; }
    }

    public class SensorDto
    {
        public System.Guid SensorId { get; set; }
        public string SensorName { get; set; }
    }



    public class DeviceSensorTemplateDto
    {
        public System.Guid DeviceSensorTemplateId { get; set; }
        public Nullable<System.Guid> DeviceId { get; set; }
        public Nullable<System.Guid> DeviceSensorId { get; set; }
        public Nullable<System.Guid> SensorTemplateId { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
    }


    public class SensorTemplateDto
    {
        public System.Guid SensorTemplateId { get; set; }
        public string SensorTemplateName { get; set; }
    }


    public class GlobalSearchDto
    {
        public System.Guid GlobalSearchId { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public string Type { get; set; }
    }



    public class SearchDataViewModel
    {
        public string Result { get; set; }
        public string FirstURL { get; set; }
        public string Text { get; set; }
        public string value { get; set; }
        public string Type { get; set; }
        public string JS_function { get; set; }
    }



    public class ActivityDTO : BaseDto
    {
        public System.Guid ActivityId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string RelatedTo { get; set; }
        public Nullable<System.Guid> RelatedToID { get; set; }
        public string RelatedToName { get; set; }

        public Nullable<System.Guid> ActivityPartyId { get; set; }
        public Nullable<System.Guid> ActivityPointerId { get; set; }
        public string Description { get; set; }





        //public virtual ICollection<EmailDTO> Emails { get; set; }
        //public virtual ICollection<MeetingDTO> Meetings { get; set; }
        //public virtual ICollection<PhoneCallDTO> PhoneCalls { get; set; }
        //public virtual ICollection<TaskDTO> Tasks { get; set; }
    }








    public class CalendarEventDTO : BaseDto
    {
        public string Description { get; set; }
        public virtual string Location { get; set; }
        public virtual DateTime OriginalStartTime { get; set; }
        public virtual DateTime Start { get; set; }
        public virtual string Summary { get; set; }
        public virtual string Kind { get; set; }
        public virtual string HangoutLink { get; set; }
        public virtual List<EventAttendee> Attendees { get; set; }
        public virtual string ColorId { get; set; }
        public virtual string CreatedRaw { get; set; }
        public virtual string HtmlLink { get; set; }
        public virtual DateTime End { get; set; }

    }



    public class EventAttendee
    {
        public virtual string Comment { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual string Email { get; set; }
    }





    public partial class ActivityAccountDTO : BaseDto
    {
        public System.Guid ActivityAccountId { get; set; }
        public Nullable<System.Guid> ActivityId { get; set; }
        public Nullable<System.Guid> AccountId { get; set; }
    }









    public class EmailDTO : BaseDto
    {
        public System.Guid EmailId { get; set; }
        public Nullable<System.Guid> ActivityId { get; set; }
    }

    public class MeetingDTO : BaseDto
    {
        public System.Guid MeetingId { get; set; }
        public Nullable<System.Guid> ActivityId { get; set; }

        public virtual ActivityDTO Activity { get; set; }
    }

    public class PhoneCallDTO : BaseDto
    {
        public System.Guid PhoneCallId { get; set; }
        public Nullable<System.Guid> ActivityId { get; set; }

        public virtual ActivityDTO Activity { get; set; }
    }

    public partial class TaskDTO : BaseDto
    {
        public System.Guid TaskId { get; set; }
        public Nullable<System.Guid> ActivityId { get; set; }

        public virtual ActivityDTO Activity { get; set; }
    }


    public class WorkFlowDTO : BaseDto
    {
        public System.Guid WorkFlowId { get; set; }
        public string Name { get; set; }
        public string TriggerCondition { get; set; }
        public Nullable<System.DateTime> TriggerIn { get; set; }
        public Nullable<System.DateTime> TriggerOut { get; set; }
        public string TargetOn { get; set; }
        public string Description { get; set; }
        public string WorkFlowStatus { get; set; }
        public string AppliedTo { get; set; }
        public Nullable<int> Frequency { get; set; }
        public Nullable<int> FrequencyOut { get; set; }
        public Nullable<int> Priority { get; set; }
        public string WorkFlowDesign { get; set; }
        public string Action { get; set; }
    }

    public class WorkFlowReportDTO : BaseDto
    {
        public System.Guid WorkFlowReportId { get; set; }
        public Nullable<System.Guid> WorkFlowId { get; set; }
        public string WorkFlowStatus { get; set; }
        public string Action { get; set; }
        public string WorkFlowActionStatus { get; set; }
        public string WorkFlowDesign { get; set; }
        public string AppliedTo { get; set; }
        public Nullable<int> Frequency { get; set; }
        public Nullable<int> Priority { get; set; }

        public virtual WorkFlowDTO WorkFlow { get; set; }
    }

    public class WorkFlowNodeDTO
    {
        public System.Guid NodeDataId { get; set; }
        public string text { get; set; }
        public string key { get; set; }
        public string figure { get; set; }
        public string fill { get; set; }
        public string loc { get; set; }
    }



    public class WorkFlowMappingDTO : BaseDto
    {
        public System.Guid WorkFlowMappingId { get; set; }
        public Nullable<System.Guid> WorkFlowId { get; set; }
        public string SourceType { get; set; }
        public string SourceColumn { get; set; }
        public string SourceValue { get; set; }
        public string SourceData { get; set; }
        public string Action { get; set; }
        public Nullable<bool> IsDone { get; set; }
        public virtual WorkFlowDTO WorkFlow { get; set; }
    }


    public class ActivityTemplateDTO : BaseDto
    {
        public System.Guid ActivityTemplateId { get; set; }
        public string ActivityTemplateType { get; set; }
        public string PropertyName { get; set; }
        public string PropertyValue { get; set; }
        public string PropertyType { get; set; }
    }



    public class workflowDataTypeDTO
    {
        public string ColumnName { get; set; }
        public string DataType { get; set; }
    }


    public class ActionColumnAndValuesDto
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class WorkFlowTypeDDdto
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }



    public class MeetingActionDTO
    {
        public string Name { set; get; }
        public string Email { set; get; }
        public string Location { set; get; }
        public DateTime StartDate { set; get; }
        public DateTime EndDate { set; get; }
        public string Subject { set; get; }
        public string Body { set; get; }
    }


    public class EventMonitorDTO
    {
        public System.Guid EventMonitorId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public Nullable<bool> Status { get; set; }
        public string Message { get; set; }
        public string Path { get; set; }
        public string Color { get; set; }
        public string IPAddress { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class EventLogDTO
    {
        public System.Guid EventLogId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public Nullable<bool> Status { get; set; }
        public string Message { get; set; }
        public string Path { get; set; }
        public string Color { get; set; }
        public string IPAddress { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class EventNotificationDTO
    {
        public System.Guid EventNotificationId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public Nullable<bool> Status { get; set; }
        public string Message { get; set; }
        public string Path { get; set; }
        public string Color { get; set; }
        public string IPAddress { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }


    public partial class EmailConfigurationDTO
    {
        public System.Guid EmailConfigurationId { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }



    public partial class EmailTemplateDTO
    {
        public System.Guid EmailTemplateId { get; set; }
        public Nullable<System.Guid> EmailConfigurationId { get; set; }
        public Nullable<System.Guid> WorkFlowId { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

        public virtual EmailConfigurationDTO EmailConfiguration { get; set; }
        public virtual WorkFlowDTO WorkFlow { get; set; }
    }

    public partial class ProductCatelogDTO : BaseDto
    {
        public System.Guid ProductId { get; set; }
        public Nullable<long> SerialNumber { get; set; }
        public string ProductName { get; set; }
        public Nullable<System.Guid> CategoryId { get; set; }
        public Nullable<System.DateTime> ValidFrom { get; set; }
        public Nullable<System.DateTime> ValidTo { get; set; }
        public string Description { get; set; }
        public string ProductNote { get; set; }

    }

    public partial class ProductPriceListDTO : BaseDto
    {
        public System.Guid ProductPriceId { get; set; }
        public Nullable<System.Guid> CurrencyId { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<System.Guid> ProductId { get; set; }
        public string Description { get; set; }
        public virtual CurrencyDto Currency { get; set; }
        public virtual ProductCatelogDTO ProductCatelog { get; set; }
    }

    public partial class DiscountDTO : BaseDto
    {
        public System.Guid DiscountId { get; set; }
        public string Name { get; set; }
        public Nullable<decimal> MinQuantity { get; set; }
        public Nullable<decimal> MaxQuantity { get; set; }
        public Nullable<System.Guid> ProductPriceId { get; set; }
        public Nullable<decimal> DiscountAmount { get; set; }
        public Nullable<decimal> DiscountPercentage { get; set; }
        public string Description { get; set; }
    }





    #region Enum Classess


    public enum ActivityType
    {
        Email,
        Meeting,
        PhoneCalls
    }


    public enum RelatedToEnum
    {
        Account,
        Oppertunities,
        Leads
    }



    public enum EntityTypes
    {
        Account,
        Lead,
        Oppertunity,
        Device,
        WorkOrder,
        WorkFlow,
        WorkFlowReport,
        ActivityTemplate,
        WorkFlowMapping,
        EventLog,
        EventMonitor,
        EventNotification,
        EmailConfiguration,
        EmailTemplate,
        GlobalSearch,
        Industry, Location,
        Reading,
        ReadingType,
        Resource,
        Team,
        User, Address, Alert,
        AccountSize, CustomerAsset, ServiceCall, ReadingUnit
    }

    public static class EventType
    {
        public const string Log = "Log";
        public const string Exception = "Exception";
        public const string Notification = "Notification";
    }

    public static class EventColor
    {
        public const string yellow = "yellow";
        public const string red = "red";
        public const string green = "green";
    }


    public static class DeviceMaintenance
    {
        public const string None = "None";
        public const string IsRepaired = "IsRepaired";
        public const string IsServiced = "IsServiced";
    }



    public static class CloudServiceForDD
    {
        public const string INCA = "INCA";
        public const string IBM = "IBM";
        public const string Google = "Google";
        public const string Microsoft = "Microsoft";
        public const string Amazon = "Amazon";
    }


    public static class ExcludedColumns
    {
        public const string StatusId = "StatusId";
        public const string AccountId = "AccountId";
        public const string LeadId = "LeadId";
        public const string DeviceId = "DeviceId";
        public const string AssignedUser = "AssignedUser";
        public const string AssignedTeam = "AssignedTeam";
        public const string CreatedBy = "CreatedBy";
        public const string CreatedDate = "CreatedDate";
        public const string UpdatedBy = "UpdatedBy";
        public const string UpdatedDate = "UpdatedDate";
    }

    









    public static class TrigegrConditionConstant
    {
        public const string Pre_Event = "Pre Event";
        public const string Post_Event = "Post Event";
    }

    public static class appliedToConstant
    {
        public const string OnCreate = "OnCreate";
        public const string OnUpdate = "OnUpdate";
        public const string CreateAndUpdate = "Create And Update";
    }
    public static class WFStatusConstant
    {
        public const string In_Progress = "In Progress";
        public const string Waiting_for_User_Action = "Waiting for User Action";
        public const string Paused = "Paused";
        public const string Completed = "Completed";
        public const string Cancelled = "Cancelled";
        public const string Error = "Error";
    }






    public static class WFActionConstant
    {
        public const string Email = "Email";
        public const string Note = "Note";
        public const string Alert = "Alert";
        public const string Meeting = "Meeting";
        public const string Account = "Account";
        public const string Lead = "Lead";
    }

   



    public static class ActionStatusConstant
    {
        public const string Success = "Success";
        public const string Failure = "Failure";
    }


    #endregion



}
