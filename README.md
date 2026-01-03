## 3DPrintingVContainerSample

Demo project about 3D printing management where I showcase my architecture: composition via VContainer (DI), reactive events with R3, async workflows with UniTask, UI animations with DOTween, configs via ScriptableObjects, and core systems (Cinemachine/NavMesh).

## Technologies & packages

- Unity 6 + URP
- VContainer — dependency injection and scene composition (LifetimeScope, EntryPoint/IInitializable)
- R3 — reactive events/subscriptions (Subject, Observable.EveryUpdate, CompositeDisposable)
- UniTask — allocation-free async/await, including UI transitions
- DOTween — UI animations, integrated via ToUniTask()
- Lean Pool — object pooling for efficient instantiation/reuse of GameObjects
- ScriptableObjects — configs (actions/shop/camera/workers/windows)
- Unity Input System, Cinemachine, AI Navigation (NavMesh)
