using LiveSplit.Model;
using LiveSplit.UI.Components;
using System;

[assembly: ComponentFactory(typeof(HenkMedalsComponentFactory))]

namespace LiveSplit.UI.Components
{
    public class HenkMedalsComponentFactory : IComponentFactory
    {
        public string ComponentName => "Action Henk Medals";

        public string Description => "Displays the current level's name and rainbow medal time";

        public ComponentCategory Category => ComponentCategory.Information;

        public IComponent Create(LiveSplitState state) => new HenkMedalsComponent(state);

        public string UpdateName => ComponentName;

        public string XMLURL => "";

        public string UpdateURL => "";

        public Version Version => Version.Parse("0.1.0");
    }
}
