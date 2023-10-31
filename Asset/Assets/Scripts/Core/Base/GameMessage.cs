using System;
using System.Collections.Generic;

public class Act {
    public int id;
    public Delegate handler;

    public Act(int id, Delegate handler) {
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

    public void Invoke<T1, T2, T3>(T1 t1, T2 t2, T3 t3) {
        ((Action<T1, T2, T3>)handler).Invoke(t1, t2, t3);
    }
}

public class GameMessage {
    private MessageRegister register = new MessageRegister();
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

public class MessageRegister {
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

public class GameMessageCode {
    public static MessageCode CreateObject = MessageCode.CreateObject;//创建一个物体
}

[Serializable]
public enum MessageCode {
    CreateObject = 1,//创建一个物体
}