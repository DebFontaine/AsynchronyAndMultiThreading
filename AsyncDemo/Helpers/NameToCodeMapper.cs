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
        List<MethodObject> _methodObjects = new();
        Dictionary<string, string> _methodCodeDictionary = new();
        Dictionary<string, string> _methodCommentDictionary = new();

        public List<MethodObject> MethodNumberList { get {  return _methodObjects; } }
        public Dictionary<string, string> MethodNameCodeMap { get { return _methodCodeDictionary; } }

        public Dictionary<string, string> MethodCommentDictionary {  get { return _methodCommentDictionary; } }

        public NameToCodeMapper(string jsonFilePath)
        {
            _methodObjects = GetMapFromJson(jsonFilePath);
            _methodCodeDictionary = _methodObjects.ToDictionary(method => method.Name, method => method.Code);
            _methodCommentDictionary = _methodObjects.ToDictionary(method => method.Name, method => method.Comments);
        }

        private List<MethodObject> GetMapFromJson(string jsonFilePath)
        {
            List<MethodObject> methodObjects = new();
            try
            {
                string jsonText = File.ReadAllText(jsonFilePath);

                // Deserialize JSON into MethodObject
                methodObjects = JsonSerializer.Deserialize<List<MethodObject>>(jsonText);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error getting name to code map" + e.Message);
            }


            return methodObjects;
        }
    }
}
