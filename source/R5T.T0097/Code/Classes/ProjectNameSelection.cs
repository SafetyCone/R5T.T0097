using System;

using R5T.T0092;


namespace R5T.T0097
{
    public class ProjectNameSelection : IMutableNamedIdentified
    {
        public Guid ProjectIdentity { get; set; }
        public string ProjectName { get; set; }

        string INamed.Name => this.ProjectName;
        string IMutableNamed.Name { get => this.ProjectName; set => this.ProjectName = value; }
        Guid IIdentified.Identity => this.ProjectIdentity;
        Guid IMutableIdentified.Identity { get => this.ProjectIdentity; set => this.ProjectIdentity = value; }


        public override string ToString()
        {
            var representation = Instances.NamedIdentifiedOperator.GetRepresentation(this);
            return representation;
        }
    }
}
