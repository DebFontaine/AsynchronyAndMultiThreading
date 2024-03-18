using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AsyncDemo.Helpers
{
    public class NameToCodeMapper
    {
        List<CodeDefinitions> _methodObjects = new();
        Dictionary<string, string> _methodCodeDictionary = new();
        Dictionary<string, string> _methodCommentDictionary = new();

        public List<CodeDefinitions> MethodNumberList { get {  return _methodObjects; } }
        public Dictionary<string, string> MethodNameCodeMap { get { return _methodCodeDictionary; } }

        public Dictionary<string, string> MethodCommentDictionary {  get { return _methodCommentDictionary; } }

        public NameToCodeMapper(string jsonFilePath)
        {
            _methodObjects = GetMapFromJson(jsonFilePath);
            _methodCodeDictionary = _methodObjects.ToDictionary(method => method.Name, method => method.Code);
            _methodCommentDictionary = _methodObjects.ToDictionary(method => method.Name, method => method.Comments);
        }

        private List<CodeDefinitions> GetMapFromJson(string jsonFilePath)
        {
            List<CodeDefinitions> methodObjects = new();
            try
            {
                string jsonText = File.ReadAllText(jsonFilePath);

                // Deserialize JSON into MethodObject
                methodObjects = JsonSerializer.Deserialize<List<CodeDefinitions>>(jsonText);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error getting name to code map" + e.Message);
            }


            return methodObjects;
        }
    }
}
