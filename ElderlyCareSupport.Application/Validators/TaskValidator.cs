using ElderlyCareSupport.Domain.Models;
using FluentValidation;

namespace ElderlyCareSupport.Application.Validators;

public sealed class TaskValidator : AbstractValidator<TaskDetails>
{
    public TaskValidator()
    {
        RuleFor(task => task.TaskId)
            .NotEmpty()
            .WithMessage("Task Id cannot be empty");
        
        RuleFor(task => task.TaskName)
            .NotEmpty().WithMessage("Task Name cannot be empty");
        
        RuleFor(task => task.TaskDescription)
            .NotEmpty().WithMessage("Task Description cannot be empty");
        
        RuleFor(task => task.StartDate)
            .NotEmpty().WithMessage("Task Start Date cannot be empty")
            .GreaterThanOrEqualTo(DateTime.Now).WithMessage("Task Start Date should be greater or equal to DateTime.Now");
        
        
        RuleFor(task => task.EndDate)
            .NotEmpty().WithMessage("Task End date cannot be empty")
            .GreaterThan(DateTime.Now).WithMessage("Task End Date should be greater or equal to DateTime.Now");
        
        RuleFor(task => task.TaskStatusId)
            .NotEmpty().WithMessage("Task Status cannot be empty");
        
        RuleFor(task => task.ElderlyPersonId)
            .NotEmpty().WithMessage("Task Elder Person cannot be empty");
        
        RuleFor(task => task.CreatedOn)
            .NotEmpty().WithMessage("Task Created on cannot be empty")
            .Equal(DateTime.Now.Date.Date).WithMessage("Task Created on cannot be equal to DateTime.Now");
        
        RuleFor(task => task.ModifiedOn)
            .NotEmpty().WithMessage("Task End date cannot be empty")
            .GreaterThan(DateTime.Now.Date.Date).WithMessage("Task End date should be greater or equal to DateTime.Now");
        
    }
}