using ElderlyCareSupport.Application.Contracts.Requests;
using FluentValidation;

namespace ElderlyCareSupport.Application.Validators;

public sealed class TaskCreationRequestValidator : AbstractValidator<TaskCreationRequest>
{
    public TaskCreationRequestValidator()
    {
        RuleFor(x => x.TaskName).NotEmpty().WithMessage("Task name cannot be empty");
        RuleFor(x => x.TaskDescription).NotEmpty().WithMessage("Task description cannot be empty");
        RuleFor(x => x.CreationDate)
            .NotEmpty().WithMessage("Creation date cannot be empty")
            .GreaterThanOrEqualTo(DateTime.Now.Date).WithMessage("Creation date cannot be greater than now");
        
        RuleFor(x => x.EndDate)
            .NotEmpty().WithMessage("End date cannot be empty")
            .GreaterThan(DateTime.Now.Date).WithMessage("End Date cannot be greater than now");
        
        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("Start date cannot be empty")
            .GreaterThanOrEqualTo(DateTime.Now.Date).WithMessage("Start date cannot be greater than now");
        
        RuleFor(x => x.ModificationDate)
            .NotEmpty().WithMessage("Modification date cannot be empty")
            .GreaterThan(DateTime.Now.Date).WithMessage("Modification date cannot be greater than now");

        RuleFor(x => x.ElderlyId)
            .NotEmpty().WithMessage("Elderly id cannot be empty");
    }
    
}