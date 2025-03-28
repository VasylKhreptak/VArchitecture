﻿using System;
using UniRx;
using VContainer.Unity;

namespace Infrastructure.Services.Framerate.Core
{
    public interface IFramerateService : IInitializable, IDisposable
    {
        public IReadOnlyReactiveProperty<float> AverageFramerate { get; }

        public void SetTargetFramerate(int framerate);
    }
}