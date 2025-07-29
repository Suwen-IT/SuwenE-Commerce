namespace Application.Interfaces.Services;

public interface ITemplateService
{
    Task<string>LoadTemplateAsync(string templateName);
    
    string ParseTemplate(string template,Dictionary<string, string> parameters);
}