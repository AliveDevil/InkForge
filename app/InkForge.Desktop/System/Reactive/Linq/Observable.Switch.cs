using System.Reactive.Disposables;

namespace System.Reactive.Linq;

public static class ObservableSwitch
{
	public static IObservable<T> Switch<T>(this IObservable<IObservable<T>> observable, T defaultValue)
	{
		return new SwitchObservable<T>(observable, defaultValue);
	}

	private class SwitchObservable<T>(IObservable<IObservable<T>> sources, T defaultValue) : ObservableBase<T>
	{
		protected override IDisposable SubscribeCore(IObserver<T> observer)
		{
			_ _ = new(defaultValue, observer);
			_.Run(sources);
			return _;
		}

		private class _(T defaultValue, IObserver<T> observer) : ObserverBase<IObservable<T>>
		{
			private readonly SerialDisposable _innerSerialDisposable = new();
			private readonly SingleAssignmentDisposable _upstream = new();
			private bool _hasLatest;
			private int _latest;
			private bool _stopped = false;

			public void Run(IObservable<IObservable<T>> sources)
			{
				_upstream.Disposable = sources.Subscribe(this);
				if (_innerSerialDisposable.Disposable is null)
				{
					observer.OnNext(defaultValue);
				}
			}

			protected override void Dispose(bool disposing)
			{
				if (disposing)
				{
					_innerSerialDisposable?.Dispose();
				}

				base.Dispose(disposing);
			}

			protected void ForwardOnCompleted() => observer.OnCompleted();

			protected void ForwardOnError(Exception error) => observer.OnError(error);

			protected void ForwardOnNext(T value) => observer.OnNext(value);

			protected override void OnCompletedCore()
			{
				_upstream.Dispose();
				_stopped = true;

				if (!_hasLatest)
				{
					observer.OnCompleted();
				}
			}

			protected override void OnErrorCore(Exception error) => ForwardOnError(error);

			protected override void OnNextCore(IObservable<T> value)
			{
				uint id = unchecked((uint)Interlocked.Increment(ref _latest));
				_hasLatest = true;

				var innerObserver = new InnerObserver(this, id, defaultValue);
				_innerSerialDisposable.Disposable = innerObserver;
				innerObserver.Subscribe(value);
			}

			private class InnerObserver(_ parent, uint id, T defaultValue) : ObserverBase<T>
			{
				private readonly SingleAssignmentDisposable _upstream = new();

				public bool Found { get; set; } = false;

				public void Subscribe(IObservable<T> upstream)
				{
					_upstream.Disposable = upstream.SubscribeSafe(this);
					if (!Found)
					{
						OnNext(defaultValue);
					}
				}

				protected override void Dispose(bool disposing)
				{
					if (disposing)
					{
						_upstream.Dispose();
					}

					base.Dispose(disposing);
				}

				protected override void OnCompletedCore()
				{
					Dispose();

					if (parent._latest == id)
					{
						parent._hasLatest = false;
						if (!Found)
						{
							OnNextCore(defaultValue);
						}

						if (parent._stopped)
						{
							parent.ForwardOnCompleted();
						}
					}
				}

				protected override void OnErrorCore(Exception error)
				{
					Dispose();

					if (parent._latest == id)
					{
						if (!Found)
						{
							OnNextCore(defaultValue);
						}
						
						parent.ForwardOnError(error);
					}
				}

				protected override void OnNextCore(T value)
				{
					Found = true;
					if (parent._latest == id)
					{
						parent.ForwardOnNext(value);
					}
				}
			}
		}
	}
}
