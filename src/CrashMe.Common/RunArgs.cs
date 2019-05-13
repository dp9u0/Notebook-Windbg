using System.Collections.Generic;

namespace CrashMe.Common
{

    /// <summary>
    /// Argument For Run Crasher
    /// </summary>
    public class RunArgs
    {

        // field to store arguments
        private readonly List<string> _arguments;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="args">arguments</param>
        public RunArgs(params string[] args)
        {
            _arguments = new List<string>(args);
        }

        public IEnumerable<string> Arguments => _arguments;

        /// <summary>
        /// Get First String Argument
        /// </summary>
        /// <param name="argument">out value for argument</param>
        /// <returns>got or not</returns>
        public bool TryGetFirst(out string argument)
        {
            return TryGet(0, out argument);
        }

        /// <summary>
        /// Get Second String Argument
        /// </summary>
        /// <param name="argument">out value for argument</param>
        /// <returns>got or not</returns>
        public bool TryGetSecond(out string argument)
        {
            return TryGet(1, out argument);
        }

        /// <summary>
        /// Get First Int Argument
        /// </summary>
        /// <param name="argument">out value for argument</param>
        /// <returns>got or not</returns>
        public bool TryGetFirstAsInt(out int argument)
        {
            return TryGetInt(0, out argument);
        }

        /// <summary>
        /// Get Second Int Argument
        /// </summary>
        /// <param name="argument">out value for argument</param>
        /// <returns>got or not</returns>
        public bool TryGetSecondAsInt(out int argument)
        {
            return TryGetInt(1, out argument);
        }

        /// <summary>
        /// Get Origin String Argument By Index
        /// </summary>
        /// <param name="index">index of argument to get</param>
        /// <param name="argument">out value for argument</param>
        /// <returns>got or not</returns>
        public bool TryGet(int index, out string argument)
        {
            argument = string.Empty;
            if (_arguments.Count > index)
            {
                argument = _arguments[index];
                return true;
            }

            return false;
        }

        /// <summary>
        /// Get Argument As Int By Index
        /// </summary>
        /// <param name="index">index of argument to get</param>
        /// <param name="argument">out value for argument</param>
        /// <returns>got or not</returns>
        public bool TryGetInt(int index, out int argument)
        {
            argument = default;
            if (_arguments.Count > index) return int.TryParse(_arguments[index], out argument);
            return false;
        }

    }

}