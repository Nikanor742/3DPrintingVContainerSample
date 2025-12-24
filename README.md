Demo project about 3D printing management where I showcase my architecture: composition via VContainer (DI), reactive events with R3, async workflows with UniTask, UI animations with DOTween, configs via ScriptableObjects, and core systems (Input/Cinemachine/NavMesh). The code in this repo is cloned from my private repository; I plan to keep expanding the main repo and don’t want to make it public.

Technologies & packages
Unity 6 + URP
VContainer — dependency injection and scene composition (LifetimeScope, EntryPoint/IInitializable)
R3 — reactive events/subscriptions (Subject, Observable.EveryUpdate, CompositeDisposable)
UniTask — allocation-free async/await, including UI transitions
Lean Pool — object pooling for efficient instantiation/reuse of GameObjects
DOTween — UI animations, integrated via ToUniTask()
ScriptableObjects — configs (actions/shop/camera/workers/windows)
Cinemachine, AI Navigation (NavMesh)
