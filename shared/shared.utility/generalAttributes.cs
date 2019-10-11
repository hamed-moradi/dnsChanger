using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

namespace shared.utility {
    public class SchemaAttribute: Attribute {
        public string Name { get; set; }
        public SchemaAttribute(string name = null) {
            Name = name;
        }
    }
    public class InputParameterAttribute: Attribute {
        public string Name { get; set; }
        public InputParameterAttribute(string name = null) {
            Name = name;
        }
    }
    public class OutputParameterAttribute: Attribute {
        public string Name { get; set; }
        public OutputParameterAttribute(string name = null) {
            Name = name;
        }
    }
    public class ReturnParameterAttribute: Attribute { }
    public class ErrorAttribute: Attribute {
        public string Message { get; set; }

        public ErrorAttribute(string message) {
            Message = message;
        }
    }
    public class HelperParameterAttribute: Attribute { }
    public class SearchAttribute: Attribute {
        public SearchFieldType Type { get; }
        public SearchAttribute(SearchFieldType type = SearchFieldType.String) {
            Type = type;
        }
    }
}
