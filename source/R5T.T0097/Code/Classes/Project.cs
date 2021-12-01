using System;


namespace R5T.T0097
{
    public class Project : IProject
    {
        public Guid Identity { get; set; }

        public string Name { get; set; }
        public string FilePath { get; set; }


        public override string ToString()
        {
            var representation = Instances.NamedIdentifiedOperator.GetRepresentation(this);
            return representation;
        }
    }
}
