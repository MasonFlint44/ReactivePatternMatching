﻿using System;

namespace ReactiveExperiment
{
    public class Subscriber<T>: IObserver<T>
    {
        private readonly Action<T> _onNext;
        private readonly Action<Exception> _onError;
        private readonly Action _onCompleted;

        public Subscriber(Action<T> onNext)
        {
            _onNext = onNext;
        }

        public Subscriber(Action<T> onNext, Action<Exception> onError, Action onCompleted)
        {
            _onNext = onNext;
            _onError = onError;
            _onCompleted = onCompleted;
        }

        public void OnNext(T value)
        {
            _onNext?.Invoke(value);
        }

        public void OnError(Exception error)
        {
            _onError?.Invoke(error);
        }

        public void OnCompleted()
        {
            _onCompleted?.Invoke();
        }
    }
}
