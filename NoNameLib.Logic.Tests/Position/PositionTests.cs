using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NoNameLib.Logic.Tests.Position
{
    [TestClass]
    public class PositionTests
    {
        [TestMethod]
        public void ValueOutOfRange()
        {
            var postition = new Logic.Position.Position();
            try
            {
                postition.Z = -1;
                Assert.Fail("Position.Z value has to be equal or greater than 0");
            }
            catch
            {
                // Eat exception as this is excepted behaviour
            }
        }

        [TestMethod]
        public void PositionHashing()
        {
            var positions = new List<Logic.Position.Position>()
                {
                    new Logic.Position.Position(6543, 5432, 1234),
                    new Logic.Position.Position(11, 22, 35),
                    new Logic.Position.Position(-50, -256, 13)
                };

            foreach (var position in positions)
            {
                var hash = position.GetHash();
                var newPosition = new Logic.Position.Position(hash);

                Assert.IsTrue(newPosition.Equals(position), "Position '{0}', New Position '{1}'", position.ToString(), newPosition,ToString());
            }
        }
    }
}
