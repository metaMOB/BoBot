using System;

public abstract class BoBot_FSMState {
	private BoBot_FSMState nextState;
	public string id = "test";
	
	public abstract void init();
	
	public abstract BoBot_FSMState transition();
}


