﻿//*********************************************************//
//    Copyright (c) Microsoft. All rights reserved.
//    
//    Apache 2.0 License
//    
//    You may obtain a copy of the License at
//    http://www.apache.org/licenses/LICENSE-2.0
//    
//    Unless required by applicable law or agreed to in writing, software 
//    distributed under the License is distributed on an "AS IS" BASIS, 
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or 
//    implied. See the License for the specific language governing 
//    permissions and limitations under the License.
//
//*********************************************************//

using System.Collections.Generic;
using Microsoft.VisualStudioTools.Project;
using Newtonsoft.Json.Linq;

namespace Microsoft.NodejsTools.Debugger.Serialization {
    sealed class NodePrototypeVariable : INodeVariable {
        public NodePrototypeVariable(NodeEvaluationResult parent, JToken prototype, Dictionary<int, JToken> references) {
            Utilities.ArgumentNotNull("prototype", prototype);
            Utilities.ArgumentNotNull("references", references);

            Id = (int)prototype["ref"];
            JToken reference;
            if (!references.TryGetValue(Id, out reference)) {
                reference = prototype;
            }
            Parent = parent;
            StackFrame = parent != null ? parent.Frame : null;
            Name = NodeVariableType.Prototype;
            TypeName = (string)reference["type"];
            Value = (string)reference["value"];
            Class = (string)reference["className"];
            Text = (string)reference["text"];
            Attributes = NodePropertyAttributes.DontEnum;
            Type = NodePropertyType.Normal;
        }

        public int Id { get; private set; }
        public NodeEvaluationResult Parent { get; private set; }
        public string Name { get; private set; }
        public string TypeName { get; private set; }
        public string Value { get; private set; }
        public string Class { get; private set; }
        public string Text { get; private set; }
        public NodePropertyAttributes Attributes { get; private set; }
        public NodePropertyType Type { get; private set; }
        public NodeStackFrame StackFrame { get; private set; }
    }
}