using System;
using System.Collections.Generic;

public class FAct {
    public int id;
    public Delegate handler;

    public FAct(int id, Delegate handler) {
        this.id = id;
        this.handler = handler;
    }

    public void Invoke() {
        ((Action)handler).Invoke();
    }

    public void Invoke<T>(T t) {
        ((Action<T>)handler).Invoke(t);
    }

    public void Invoke<T1, T2>(T1 t1, T2 t2) {
        ((Action<T1, T2>)handler).Invoke(t1, t2);
    }
}

public class FGameMessage {
    private FMessageRegister register = new FMessageRegister();
    public void Reg(int id, Action a) {
        register.Register(id, a);
    }

    public void Reg<T>(int id, Action<T> a) {
        register.Register(id, a);
    }

    public void Reg<T1, T2>(int id, Action<T1, T2> a) {
        register.Register(id, a);
    }

    public void UnReg(int id, Action act) {
        register.UnRegister(id, act);
    }

    public void UnReg<T>(int id, Action<T> act) {
        register.UnRegister(id, act);
    }

    public void UnReg<T1, T2>(int id, Action<T1, T2> act) {
        register.UnRegister(id, act);
    }

    public void Dis(int id) {
        register.Dispatcher(id);
    }

    public void Dis<T>(int id, T t) {
        register.Dispatcher(id, t);
    }

    public void Dis<T1, T2>(int id, T1 t1, T2 t2) {
        register.Dispatcher(id, t1, t2);
    }
}

public class FMessageRegister {
    private Dictionary<int, List<Act>> handles = new Dictionary<int, List<Act>>();

    public void Register(int id, Delegate e) {
        List<Act> acts;
        if (!handles.TryGetValue(id, out acts)) {
            acts = new List<Act>();
            handles.Add(id, acts);
        }

        if (SearchWrapperIndex(acts, e) == -1) {
            acts.Add(new Act(id, e));
        }
    }

    private int SearchWrapperIndex(List<Act> wps, Delegate handler) {
        int length = wps.Count;
        for (int i = 0; i < length; ++i) {
            if (wps[i].handler == handler) {
                return i;
            }
        }
        return -1;
    }

    public void UnRegister(int id, Delegate handler) {
        if (handler == null) {
            return;
        }

        List<Act> acts;
        if (handles.TryGetValue(id, out acts)) {
            int index = SearchWrapperIndex(acts, handler);
            if (index >= 0) {
                acts.RemoveAt(index);
                handles[id] = acts;
            }
        }
    }

    public void Dispatcher(int id) {
        if (handles.TryGetValue(id, out var acts)) {
            for (int i = 0; i < acts.Count; i++) {
                acts[i].Invoke();
            }
        }
    }

    public void Dispatcher<T>(int id, T act) {
        if (handles.TryGetValue(id, out List<Act> acts)) {
            for (int i = 0; i < acts.Count; i++) {
                acts[i].Invoke(act);
            }
        }
    }

    public void Dispatcher<T1, T2>(int id, T1 act1, T2 act2) {
        if (handles.TryGetValue(id, out List<Act> acts)) {
            for (int i = 0; i < acts.Count; i++) {
                acts[i].Invoke(act1, act2);
            }
        }
    }
}

public class FMessageCode {
    public static int StartGame = 5;//开始游戏
    public static int QuitGame = 6;//退出游戏

    public static int UpdateEvent = 12;//更新

    public static int CreateTerrain = 3;//创建地形
    public static int RemoveTerrain = 4;//移除地形
    public static int RemoveAllTerrain = 8;//移除所有物体

    public static int CreatePlayer = 9;//创建玩家
    public static int RemovePlayer = 10;//移除玩家
    public static int RemoveAllPlayer = 11;//移除所有玩家

    public static int CreateComponent = 13;//注册组件
    public static int RemoveComponent = 14;//移除组件
    public static int RemoveAllComponent = 15;//移除所有组件
}