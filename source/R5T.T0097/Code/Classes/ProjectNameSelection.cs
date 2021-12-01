using System;

using R5T.T0092;


namespace R5T.T0097
{
    public class ProjectNameSelection : INamedIdentified
    {
        public Guid ProjectIdentity { get; set; }
        public string ProjectName { get; set; }

        string INamed.Name => this.ProjectName;
        Guid IIdentified.Identity => this.ProjectIdentity;


        public override string ToString()
        {
            var representation = Instances.NamedIdentifiedOperator.GetRepresentation(this);
            return representation;
        }
    }
}
