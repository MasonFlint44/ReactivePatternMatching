using System;
using System.Collections.Generic;

namespace ReactiveExperiment
{
    public class Subscription<T>: IObservable<T>
    {
        public readonly HashSet<IObserver<T>> Subscribers = new HashSet<IObserver<T>>();

        public IDisposable Subscribe(IObserver<T> observer)
        {
            if (!Subscribers.Contains(observer))
            {
                Subscribers.Add(observer);
            }
            return new Unsubscriber<T>(Subscribers, observer);
        }

        public IDisposable Subscribe(Action<T> onNext)
        {
            var observer = new Subscriber<T>(onNext);
            return Subscribe(observer);
        }

        public IDisposable Subscribe(Action<T> onNext, Action<Exception> onError, Action onCompleted)
        {
            var observer = new Subscriber<T>(onNext, onError, onCompleted);
            return Subscribe(observer);
        }

        public void Broadcast(T notification)
        {
            foreach (var subscriber in Subscribers)
            {
                subscriber.OnNext(notification);
            }
        }

        public void BroadcastError(Exception error)
        {
            foreach (var subscriber in Subscribers)
            {
                subscriber.OnError(error);
            }
        }

        public void EndBroadcast()
        {
            foreach (var subscriber in Subscribers)
            {
                subscriber.OnCompleted();
            }
        }
    }
}
