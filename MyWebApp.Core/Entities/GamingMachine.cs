using System;
using System.Collections.Generic;
using System.Text;

namespace MyWebApp.Core.Entities
{
    public class GamingMachine : BaseEntity
    {
        public override long Key() => GamingSerialNumber;

        /// <summary>
        /// Gets or sets the value of GamingSerialNumber.
        /// A unique number which will be used to identify a gaming machine.
        /// </summary>        
        public long GamingSerialNumber { get; set; }

        private int _gamingMachinePosition;
        /// <summary>
        /// Gets or sets the value of GamingMachinePostion.
        /// The range for this should be zero (default) to 1000 position.
        /// If the value specified for setting GamingMachinePostion is out of range then ArgumentOutOfRangeException will be thrown.
        /// </summary>        
        public int GamingMachinePosition {
            get =>_gamingMachinePosition;
            set
            {
                if (!IsValidPosition(value))
                    throw new ArgumentOutOfRangeException();

                _gamingMachinePosition = value;
            }
        }

        /// <summary>
        /// Gets or sets the value of GameName.
        /// </summary>        
        public string GameName { get; set; }

        public bool IsValidPosition(int position)
        {
            if (position < 0 || position > 1000)
                return false;

            return true;
        }
    }
}
