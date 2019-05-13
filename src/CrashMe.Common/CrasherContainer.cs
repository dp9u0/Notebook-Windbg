using System;
using System.Collections.Generic;
using System.Reflection;

namespace CrashMe.Common
{

    /// <summary>
    ///  Crasher Container
    /// </summary>
    public class CrasherContainer : CrasherBase
    {

        private readonly IDictionary<string, ICrasher> _crashers = new Dictionary<string, ICrasher>();

        /// <summary>
        /// Static Single Instance Pattern
        /// </summary>
        static CrasherContainer()
        {
            Instance = new CrasherContainer();
            Instance.LoadDefault();
        }

        /// <summary>
        /// Private Make Sure None Can Create A Instance
        /// </summary>
        private CrasherContainer() : base("Crasher Container", "list")
        {
        }

        /// <summary>
        /// Singleton Instance
        /// </summary>
        public static CrasherContainer Instance { get; }

        /// <summary>
        /// Help String
        /// </summary>
        public override string Help => "";

        /// <summary>
        /// Load Default Crasher
        /// </summary>
        public void LoadDefault()
        {
            Add(this);
            LoadFromAssembly(Assembly.GetExecutingAssembly(), out _);
        }

        /// <summary>
        /// Load Crasher From Assembly
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="errors">load errors</param>
        public void LoadFromAssembly(Assembly assembly, out ICollection<Exception> errors)
        {
            errors = new List<Exception>();
            var types = assembly.GetTypes();
            var crasherType = typeof(ICrasher);
            foreach (var type in types)
                if (!type.IsAbstract && crasherType.IsAssignableFrom(type) && type != typeof(CrasherContainer))
                    try
                    {
                        var crasher = (ICrasher) Activator.CreateInstance(type, true);
                        Add(crasher);
                    } catch (Exception ex)
                    {
                        errors.Add(ex);
                    }

            if (errors.Count > 0) throw new AggregateException(errors);
        }

        /// <summary>
        /// Add Crasher
        /// </summary>
        /// <param name="crasher">crasher</param>
        public void Add(ICrasher crasher)
        {
            if (_crashers.ContainsKey(crasher.Command))
                throw new InvalidOperationException($"duplicate crasher :{crasher.Name}");
            _crashers[crasher.Command] = crasher;
        }

        /// <summary>
        /// Pick A Crasher by input and Run it
        /// </summary>
        /// <param name="input">input command line and arguments</param>
        public void Run(string input)
        {
            var inputs = input.Split(' ');
            if (inputs.Length > 0)
            {
                var command = inputs[0];
                if (_crashers.ContainsKey(command))
                {
                    var crasher = _crashers[command];
                    var args = new String[inputs.Length - 1];
                    Array.ConstrainedCopy(inputs, 1, args, 0, inputs.Length - 1);
                    try
                    {
                        crasher.Run(new RunArgs(args));
                    } catch (Exception ex)
                    {
                        LoggerManager.Error(ex, $"Error When Run Crasher ${crasher.Name} : {input}");
                    }
                } else
                {
                    LoggerManager.Warn($"No Crasher To Run : {input}");
                    LoggerManager.Warn("Enter 'list' to See All Crasher...");
                }
            }
        }

        protected override void RunCore(RunArgs args)
        {
            LoggerManager.Log("Crasher Available :");
            foreach (var crasher in _crashers.Values)
            {
                LoggerManager.Warn("-----------------------------------------------------");
                LoggerManager.Log($"Name\t: {crasher.Name}");
                LoggerManager.Log($"Command\t: {crasher.Command}");
                if (!string.IsNullOrEmpty(crasher.Help))
                {
                    LoggerManager.Log("Example\t: ");
                    LoggerManager.Log($"{crasher.Help}");
                }
            }

            LoggerManager.Warn("-----------------------------------------------------");
        }

    }

}