using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;


public class playIngressGlyph : MonoBehaviour {

	private bool isMainGameInitialised;

	LinkedList<edge>[] qnInEdges;
	
	public int difficultyLevel;
	private int atGlyphNumber;
	private bool isMouseDown;
	
	public TimerForIngress timer;
	public IngressGlyphSets glyphGenerator;
	public LineRenderer lineRenderer;
	public IngressInstructionScript instructionText;
	public AudioSource sound;
	private int noOfVerticesTouched;
	int[][] qn;
	bool gameIsEnding;


	LinkedList<int> [] ans;
	bool[] correctness;

	
	private LinkedList<GameObject> selectedVertices;
	
	private bool canExitGame;

	// Use this for initialization
	IEnumerator Start () {										//needed to display qn
//	void Start(){												//needed when skipping qn display

		// used to prevent premature clicking during qn display
		isMainGameInitialised = false;
		canExitGame = false;

		//Defines how many Glyphs to show. 
		//Value: 1 to 4

		System.Random rnd = new System.Random();
		difficultyLevel = rnd.Next(1,5);

//		difficultyLevel = 2;


		//tracker for Line Renderer
		noOfVerticesTouched = 0;

		gameIsEnding = false;

		//generate qn
		qn = glyphGenerator.generateQn(difficultyLevel);

		//generate edges based on coordinates
		//to be matched against the user's answer
		qnInEdges = new LinkedList<edge>[qn.Length];
		for(int i=0; i<difficultyLevel; i++){
			qnInEdges[i] = new LinkedList<edge>();
			for(int j=0; j<qn[i].Length-1; j++){
				qnInEdges[i].AddLast(new edge(qn[i][j], qn[i][j+1]));
				}
		}

		instructionText.setColor(-1);
		instructionText.setInstruction(1);
		yield return new WaitForSeconds(0.5f);
		instructionText.setColor(0);
		yield return new WaitForSeconds(2);

		//display qn
		lineRenderer.SetColors(Color.white,Color.white);
		for(int i=0; i<difficultyLevel; i++){
			lineRenderer.SetVertexCount(qn[i].Length);
			for(int j=0; j<qn[i].Length; j++){
				GameObject v = GameObject.Find(qn[i][j].ToString());
				lineRenderer.SetPosition(j, v.transform.position);
			}
			if(i != difficultyLevel - 1){
				yield return new WaitForSeconds(2);
			}else{
				yield return new WaitForSeconds(2);
				instructionText.setColor(1);
				instructionText.setInstruction(2);
				yield return new WaitForSeconds(1.5f);
				instructionText.setColor(2);
				instructionText.setInstruction(3);
				yield return new WaitForSeconds(1);
				instructionText.setColor(3);
				instructionText.setInstruction(4);
				sound.Play();
				yield return new WaitForSeconds(1);
	
			}
		}

		lineRenderer.SetVertexCount(0);

	//	Debug.Log(difficultyLevel+" "+qn[0].Length);

		//initialise user's answer container
		ans = new LinkedList<int>[difficultyLevel];
		for(int i=0; i<difficultyLevel; i++){
			ans[i] = new LinkedList<int>();
		}

		//variables for progression through multiple glyphs
		atGlyphNumber = 0;
		isMouseDown = false;

		//release the lock, begin game
		isMainGameInitialised = true;
		timer.startTimer();
	}
	
	// Update is called once per frame
	void Update () {
		if(!gameIsEnding){
			if(!((timer.miliseconds+timer.seconds*1000)>0 && atGlyphNumber<difficultyLevel)){
				//process linkedlist into edgelist
				//match edgelist with preprepared edgelist
				//show result
				
				gameIsEnding = true; 	//lock input

				StartCoroutine(endingSeq());

			}
			
			if(Input.GetButtonUp("Fire1") && isMainGameInitialised){
				//reset
				finishedAttempt();
				atGlyphNumber++;
				lineRenderer.SetVertexCount(0);
			}
		}
/*		
		if(canExitGame == true){
			Debug.Log("Game is Ended");
		}
*/		
	}

	public bool getGameIsEnding(){
		return gameIsEnding;
	}

	public void incrementInLoopNumber(){
		atGlyphNumber++;
	}

	public void touchedVertice(int verticeName){
		ans[atGlyphNumber].AddLast(verticeName); 
		Debug.Log("List nodes: "+ans[atGlyphNumber].Count);
		++noOfVerticesTouched;
		lineRenderer.SetVertexCount(noOfVerticesTouched);
		GameObject v = GameObject.Find(verticeName.ToString());
		Debug.Log(noOfVerticesTouched+"v: "+"Position "+(noOfVerticesTouched-1)+" "+v.ToString());
		lineRenderer.SetPosition(noOfVerticesTouched-1, v.transform.position);
	}

	public void beginAttempt(){
		isMouseDown = true;
	}

	public void finishedAttempt(){
		isMouseDown = false;
		noOfVerticesTouched = 0;
	}

	public bool getIsMouseDown(){
		return isMouseDown;
	}

	public int getLastVertice(){
		if(ans[atGlyphNumber].Count==0) return -1;
		return ans[atGlyphNumber].Last.Value;
	}

	public bool getIsMainGameInitialised(){
		return isMainGameInitialised;
	}

	public IEnumerator endingSeq(){
		
		Debug.Log("Ending Game.");

		timer.stopTimer();

		yield return new WaitForSeconds(2);

		//compare results
		correctness = computeResults();
		//show results
		for(int i=0; i<difficultyLevel; i++){
			Debug.Log("Showing "+ i);
			if(correctness[i]){
				instructionText.setInstruction(5);
				instructionText.setColor(3);
				Debug.Log("Correct");
			}else{
				instructionText.setInstruction(6);
				instructionText.setColor(4);
				Debug.Log("Wrong");
			}
			lineRenderer.SetVertexCount(qn[i].Length);
			for(int j=0; j<qn[i].Length; j++){
				GameObject v = GameObject.Find(qn[i][j].ToString());
				lineRenderer.SetPosition(j, v.transform.position);
			}
			
			yield return new WaitForSeconds(2);

//			yield return StartCoroutine(waitAwhile(2));
//			StartCoroutine(waitAwhile(2));
		}

		yield return new WaitForSeconds(2);


		lineRenderer.SetVertexCount(0);
		canExitGame = true;

		//End minigame
		if(canExitGame){
			Application.LoadLevel("Win");
		}
	}
	
	private bool[] computeResults(){
		bool[] results = new bool[difficultyLevel];
		for(int i=0; i<difficultyLevel; i++){
			results[i] = false;
		}
		LinkedList<edge> ansInEdge = new LinkedList<edge>();
		for(int i=0; i<difficultyLevel; i++){
			if(ans[i].Count<2) continue;		//can't make edges with <2 vertices

			//fill up ansInEdge
			while(ans[i].Count>1){
				int v1, v2;
				v1 = ans[i].First.Value;
				ans[i].RemoveFirst();
				v2 = ans[i].First.Value;
				ansInEdge.AddLast(new edge(v1,v2));
			}	

			Debug.Log("# of Edges: "+ansInEdge.Count);

			edge[] arr1 = new edge[qnInEdges[i].Count];
			bool[] arr2 = new bool[arr1.Length];
			for(int j=0; j<arr1.Length; j++) arr2[j] = false;

			qnInEdges[i].CopyTo(arr1, 0);

			bool hasIrrelevant = false;

			//compare ansInEdge w qnInEdge
			while(ansInEdge.Count>0){
				hasIrrelevant = true;
				for(int j=0; j<arr1.Length; j++){
					if(arr1[j].isEqual(ansInEdge.First.Value)){
						arr2[j]=true;
						ansInEdge.RemoveFirst();
						hasIrrelevant = false;
						break;
					}
				}
				if(hasIrrelevant){
					break;
				}
			}

			bool checkArr2 = true;
			for(int k=0; k<arr2.Length; k++){
				if(!arr2[k]) {
					checkArr2 = false;
					break;
				}
			}
			//1st condition - check if all edges are met
			//2nd condition - check if 
			if(checkArr2 && !hasIrrelevant){
				results[i] = true;
			}
		}
		return results;
	}

	IEnumerator waitAwhile(int seconds){
		yield return new WaitForSeconds(seconds);
	}

}

public class edge {
	int x, y;
	public edge(int newX, int newY){
		x = newX;
		y = newY;
	}
	public bool isEqual(edge e){
		if((x==e.x&&y==e.y)||(y==e.x&&x==e.y)){
			return true;
		}
		return false;
	}

}