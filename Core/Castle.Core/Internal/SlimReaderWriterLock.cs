// Copyright 2004-2008 Castle Project - http://www.castleproject.org/
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace Castle.Core.Internal
{
	using System.Threading;

	public class SlimReaderWriterLock
	{
#if SILVERLIGHT
		private readonly object locker = new object();

		public SlimReaderWriterLock()
		{
		}

		public void EnterReadLock()
		{
			Monitor.Enter(locker);
		}

		public void EnterWriteLock()
		{
			Monitor.Enter(locker);
		}

		public void ExitReadLock()
		{
			Monitor.Exit(locker);
		}

		public void ExitWriteLock()
		{
			Monitor.Exit(locker);
		}
#else
		private readonly ReaderWriterLockSlim locker = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

		public void EnterReadLock()
		{
			locker.EnterUpgradeableReadLock();
		}

		public void EnterWriteLock()
		{
			locker.EnterWriteLock();
		}

		public void ExitReadLock()
		{
			locker.ExitUpgradeableReadLock();
		}

		public void ExitWriteLock()
		{
			locker.ExitWriteLock();
		}
#endif
	}
}
