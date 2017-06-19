using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathologicalGames;

public class PlayerAttack : MonoBehaviour {

	[SerializeField] private Fox fox;
	//Pool
	public string poolName;
	//public Transform ps_incendio;
	public GameObject prt;
	public circleProcess mana_bar;
	private EnergyBar eb;
	public GameObject pos;
	public GameObject incendio_ps;
	private GvrPointerPhysicsRaycaster gvrRay;
	public Snake snake;

	//fen
	public LightTransform lightTrans;

	// Use this for initialization
	void Start () {
		//eb = mana_bar.GetComponent<EnergyBar> ();
		gvrRay = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<GvrPointerPhysicsRaycaster> ();
	}
	
	// Update is called once per frame
	void Update () {

		#if UNITY_EDITOR
		if(Input.GetKeyDown (KeyCode.X) && (mana_bar.GetCurrentAmout() > 45)){
			DoMana();
			//Debug.Log("sasa0000---:" + gvrRay.isSnakeGazeIn ());
			if(gvrRay.isSnakeGazeIn () == true){
				//snake
				snake.GetDamage(40);
			}
		}
		#endif
		#if UNITY_ANDROID && !UNITY_EDITOR
		if ((GvrController.ClickButtonDown == true ) && (mana_bar.GetCurrentAmout() > 45)) {
			DoMana();
			if(gvrRay.isSnakeGazeIn () == true){
			//snake
			snake.GetDamage(40);
			}
		}
		#endif

		if(mana_bar.GetCurrentAmout() >= 100){
			mana_bar.gameObject.SetActive(false);
		}

		//if (fox.GetIsContrlEnter () == true) {

		//}
//		if(eb.valueCurrent < eb.valueMax){
//			StartCoroutine(SetValueCurToFull());
//		}
	}
	
	void DoMana(){
		// get speech from speechManager
		string speechWord = "BLINK";
		if("BLAZE".Equals(speechWord)){
			//current mana
			mana_bar.gameObject.SetActive(true);
			mana_bar.ChangeManaCir(40);
			incendio_ps.SetActive(true);
			StartCoroutine (SetManaDisper(incendio_ps));
		}else if("BLINK".Equals(speechWord)){
			mana_bar.gameObject.SetActive(true);
			mana_bar.ChangeManaCir(90);
			lightTrans.StartBlink ();
		}
	}
	
	void Attack(){
		//is Get Right Speech
		//Test use Input
		//1.Get ParticleSystem Obj from PoolManager
			 //After 3s set it Unactived
		//this.StartCoroutine(ParticleSpawner());
		incendio_ps.gameObject.SetActive(true);
		//2.inistite
		//3.fox damage
//		if (fox.GetIsContrlEnter () == true) {
//			fox.GetDamage(20);
//		}
		//4.mana circle show change
		//eb.ChangeMana(450);
		mana_bar.gameObject.SetActive(true);
		Invoke ("SetManaInvalid", 3.0f);
	}
	
	private IEnumerator SetManaDisper(GameObject obj_ps){
		yield return new WaitForSeconds(2.0f);
		obj_ps.SetActive(false);
	}

	private void SetManaInvalid(){
		mana_bar.gameObject.SetActive(false);
		incendio_ps.gameObject.SetActive(false);
	}
//	private IEnumerator ParticleSpawner(){
//		Transform inst;
//		SpawnPool shapesPool = PoolManager.Pools[this.poolName];
//		inst = shapesPool.Spawn(this.ps_incendio);
//		inst.gameObject.SetActive (true);
//		inst.parent = prt.transform;
//		//inst.position = pos.transform.position;
//		//inst.rotation = pos.transform.rotation;
//		yield return new WaitForSeconds(4.0f);
//		this.StartCoroutine(DesParticleSpawner());
//	}
//
//	private IEnumerator DesParticleSpawner()
//	{
//		SpawnPool shapesPool = PoolManager.Pools[this.poolName];
//		var spawnedCopy = new List<Transform>(shapesPool);
//		Debug.Log(shapesPool.ToString());
//		foreach (Transform instance in spawnedCopy)
//		{
//			if (instance.gameObject.activeSelf == true)
//				shapesPool.Despawn(instance);  // Internal count--
//		}
//		yield return null;
//	}

//	private IEnumerator SetValueCurToFull(){
//		while (eb.valueCurrent < eb.valueMax) {
//			eb.valueCurrent = eb.valueCurrent + 1;
//			yield return null;
//		}
//
//	}

}
