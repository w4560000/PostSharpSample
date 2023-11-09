using System;
using System.Collections.Generic;

namespace PostSharpSample.Aggregatable
{
    public class ParentObject
    {
        public int id { get; set; }
        public string parent_object_name { get; set; }
        public List<ChildObject> child_objects { get; set; }
    }

    public class ChildObject
    {
        public int id { get; set; }
        public string child_object_name { get; set; }

        public ParentObject parent_object { get; set; }
    }
}