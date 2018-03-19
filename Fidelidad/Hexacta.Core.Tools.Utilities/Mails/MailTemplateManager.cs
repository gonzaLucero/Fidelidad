using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Hexacta.Core.Tools.Utilities
{
    public class MailTemplateManager
    {
        public Dictionary<string, string> getKeysToReplace(object entity)
        {
            Dictionary<string, string> d = new Dictionary<string, string>();
            d.Add("{{urlPortal}}", ConfigurationManager.AppSettings["urlPortal"]);
            d.Add("{{url}}", ConfigurationManager.AppSettings["url"]);
            addKeysToReplace(entity, d);
            return d;
        }

        public void addKeysToReplace(string key, string value , Dictionary<string, string> d)
        {
            d.Add(string.Format("{{{{{0}}}}}", key), value);
        }


        public void addKeysToReplace(object entity, Dictionary<string, string> d)
        {
            var valoresXML = GenericUtilities.SerializeToXML(entity);
            getChildren(valoresXML.Root, d);
        }

 
        private void getChildren(XElement element, Dictionary<string, string> d)
        {
            if (element.HasElements)
            {
                foreach (var child in element.Elements())
                {
                    getChildren(child, d);
                }
            }
            else
            {
                string key = string.Join(".", element.AncestorsAndSelf().Select(e => e.Name.ToString()).ToArray().Reverse());
                if (d.Keys.Any(k => k == string.Format("{{{{{0}}}}}", key)))
                {
                    int i = 1;
                    key = string.Format("{0}_{1}", key, i);
                    while (d.Keys.Any(k => k == string.Format("{{{{{0}}}}}", key)))
                    {
                        key = string.Format("{0}_{1}", key, i);
                    }
                }
                d.Add(string.Format("{{{{{0}}}}}", key), GetValue(element));
            }
        }

        private static string GetValue(XElement element)
        {
            
            Match match = Regex.Match(element.Value, @"[0-9]{4}[(\-|\/)]\d{2}[(\-|\/)]\d{2}T\d{2}:\d{2}", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                DateTime d2;
                if (DateTime.TryParse(element.Value, null, System.Globalization.DateTimeStyles.RoundtripKind, out d2))
                    return d2.ToString("dd-MM-yyyy HH:mm");
                else
                    return element.Value;
            }
            else
            {
                return element.Value; 
            }
        }

        //public ProcessedEmail ReplaceKeysToTemplate(Dictionary<string, string> keysToReplace, int templateTypeOperation, bool? isInterno = false)
        //{
        //    Template template = _templateRepository.GetByOperacionId(templateTypeOperation, isInterno) ?? _templateRepository.GetByOperacionId(templateTypeOperation, null);
        //    return ReplaceKeysToTemplate(keysToReplace, template);
        //}

        public ProcessedEmail ReplaceKeysToTemplate(Dictionary<string, string> keysToReplace, ProcessedEmail mailToProcess)
        {
            ProcessedEmail processedMail = new ProcessedEmail();
            processedMail.Subject = ReplaceKey(mailToProcess.Subject, keysToReplace);
            if (mailToProcess.Body.IndexOf("<html>") < 0)
            {
                processedMail.Body = string.Concat("<html><head><meta charset=\"UTF-8\"><meta http-equiv=\"Content-Type\" content=\"text/html; charset=iso-8859-1\"></head><body>", ReplaceKey(mailToProcess.Body, keysToReplace), "</body></html>");
            }
            else
            {
                processedMail.Body = ReplaceKey(mailToProcess.Body, keysToReplace);
            }
            return processedMail;
        }

        public string ReplaceKey(string bodyToReplace, Dictionary<string, string> keysToReplace)
        {
            return keysToReplace.Aggregate(bodyToReplace, (current, currentKey) => current.Replace(currentKey.Key, (currentKey.Value??string.Empty)));
        }
    }
}
