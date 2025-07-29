using Application.Interfaces.Services;

namespace Suwen.Infrastructure.Services
{
    public class TemplateService : ITemplateService
    {
        private readonly string _templatesPath;

        public TemplateService()
        {
            _templatesPath = Path.Combine(Directory.GetCurrentDirectory(), "EmailTemplates");

        }

        public async Task<string> LoadTemplateAsync(string templateName)
        {
            var filePath = Path.Combine(_templatesPath, templateName);

            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Template dosyası bulunamadı: {templateName}");

            return await File.ReadAllTextAsync(filePath);
        }

        public string ParseTemplate(string template, Dictionary<string, string> parameters)
        {
            if (parameters == null)
                return template;

            foreach (var kvp in parameters)
            {
                var placeholder = "{{" + kvp.Key + "}}";
                template = template.Replace(placeholder, kvp.Value);
            }
            return template;
        }
    }
}