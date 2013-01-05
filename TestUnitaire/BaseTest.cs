using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestUnitaire
{
	public abstract class BaseTest
	{
		#region Initialize and Cleanup

		[ClassInitialize]
		public virtual void ClassInitialize()
		{
			// Make nothing if not override
		}

		[ClassCleanup]
		public virtual void ClassCleanup()
		{
			// Make nothing if not override
		}

		[TestInitialize]
		public virtual void TestInitialize()
		{
			// Make nothing if not override
		}

		[TestCleanup]
		public virtual void TestCleanup()
		{
			// Make nothing if not override
		}

		#endregion
	}
}