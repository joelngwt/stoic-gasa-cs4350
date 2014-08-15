using UnityEngine;
using System.Collections.Generic;
using System;

public class IngressGlyphSets : MonoBehaviour {

	void Update(){}
	void Start(){}


	public int[][] generateQn(int lvl){
		int[][] ans = new int[lvl][];
		Glyphs[] seq = getSequence(lvl);
		for(int i=0; i<lvl; i++){
			ans[i] = getGlyph(seq[i]);
		}
		return ans;
	}

	private Glyphs[] getSequence(int lvl){
		//generate random number, obtain a sequence corresponding to diff lvl

		//number of options for each difficulty level
		int[] optionSizes = new int[]{10,10,9,9};	//value cannot be 0

		UnityEngine.Random.seed = (int)(DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond);

		int randNo = UnityEngine.Random.Range(0,optionSizes[lvl-1]-1);
		switch(lvl){
		case 1:
			switch(randNo){
			case 1: 	return new Glyphs[]{Glyphs.ADVANCE};
			case 2: 	return new Glyphs[]{Glyphs.ATTACK};
			case 3: 	return new Glyphs[]{Glyphs.BREATHE};
			case 4: 	return new Glyphs[]{Glyphs.DESTROY};
			case 5: 	return new Glyphs[]{Glyphs.ENLIGHTEN};
			case 6: 	return new Glyphs[]{Glyphs.IMPROVE};
			case 7: 	return new Glyphs[]{Glyphs.JOURNEY};
			case 8: 	return new Glyphs[]{Glyphs.PORTAL};
			case 9: 	return new Glyphs[]{Glyphs.TRUTH};
			default: 	return new Glyphs[]{Glyphs.SHAPER};
			}
		
		case 2:
			switch(randNo){
			case 1: 	return new Glyphs[]{Glyphs.DESTROY,Glyphs.PORTAL};
			case 2: 	return new Glyphs[]{Glyphs.OPEN,Glyphs.MIND};
			case 3:		return new Glyphs[]{Glyphs.OPEN,Glyphs.PORTAL};
			case 4:		return new Glyphs[]{Glyphs.END,Glyphs.JOURNEY};
			case 5:		return new Glyphs[]{Glyphs.SEEK,Glyphs.TRUTH};
			case 6:		return new Glyphs[]{Glyphs.NOT,Glyphs.AGAIN};
			case 7:		return new Glyphs[]{Glyphs.DISCOVER, Glyphs.SELF};
			case 8: 	return new Glyphs[]{Glyphs.GAIN,Glyphs.ENLIGHTEN};
			case 9: 	return new Glyphs[]{Glyphs.JOURNEY, Glyphs.AGAIN};
			default: 	return new Glyphs[]{Glyphs.ADVANCE,Glyphs.SELF};
			}
		
		case 3: 
			switch(randNo){
			case 1: 	return new Glyphs[]{Glyphs.ATTACK,Glyphs.DESTROY,Glyphs.ADVANCE};
			case 2:		return new Glyphs[]{Glyphs.MIND,Glyphs.BODY,Glyphs.SOUL};
			case 3:		return new Glyphs[]{Glyphs.SHAPER,Glyphs.NOT,Glyphs.HUMAN};
			case 4:		return new Glyphs[]{Glyphs.MIND,Glyphs.NOT,Glyphs.OPEN};
			case 5:		return new Glyphs[]{Glyphs.PAST,Glyphs.PRESENT,Glyphs.FUTURE};
			case 6:		return new Glyphs[]{Glyphs.IMPROVE,Glyphs.SELF,Glyphs.MIND};
			case 7:		return new Glyphs[]{Glyphs.IMPROVE,Glyphs.SELF,Glyphs.BODY};
			case 8:		return new Glyphs[]{Glyphs.DESTROY,Glyphs.HUMAN,Glyphs.BODY};
			default: 	return new Glyphs[]{Glyphs.SHAPER,Glyphs.SEEK,Glyphs.TRUTH};
			}

		case 4: 
			switch(randNo){
			case 1: 	return new Glyphs[]{Glyphs.OPEN,Glyphs.PORTAL,Glyphs.SEEK, Glyphs.TRUTH};
			case 2:		return new Glyphs[]{Glyphs.OPEN,Glyphs.MIND,Glyphs.GAIN,Glyphs.ENLIGHTEN};
			case 3:		return new Glyphs[]{Glyphs.SEEK,Glyphs.TRUTH,Glyphs.GAIN,Glyphs.ENLIGHTEN};
			case 4:		return new Glyphs[]{Glyphs.IMPROVE,Glyphs.BODY,Glyphs.ADVANCE,Glyphs.AGAIN};
			case 5:		return new Glyphs[]{Glyphs.JOURNEY,Glyphs.AGAIN,Glyphs.SEEK,Glyphs.TRUTH};
			case 6:		return new Glyphs[]{Glyphs.JOURNEY,Glyphs.AGAIN,Glyphs.IMPROVE,Glyphs.MIND};
			case 7: 	return new Glyphs[]{Glyphs.DESTROY, Glyphs.PRESENT,Glyphs.DESTROY,Glyphs.FUTURE};
			case 8: 	return new Glyphs[]{Glyphs.IMPROVE,Glyphs.SELF,Glyphs.ADVANCE,Glyphs.FUTURE};
			default: 	return new Glyphs[]{Glyphs.JOURNEY,Glyphs.AGAIN,Glyphs.BREATHE,Glyphs.AGAIN};
			}

		default:		return new Glyphs[]{Glyphs.SIMPLE};

		}


	}

	private int[] getGlyph(Glyphs g){
		switch(g){
		
		case Glyphs.ADVANCE:	return new int[]{9,1,7};
		case Glyphs.AGAIN:		return new int[]{7,1,3,0,2,4};
		case Glyphs.ATTACK:		return new int[]{7,1,9,2,8};
		
		case Glyphs.BODY:		return new int[]{0,1,2,0};
		case Glyphs.BREATHE: 	return new int[]{5,1,0,2,6};

		case Glyphs.DESTROY: 	return new int[]{5,1,0,4,8};
		case Glyphs.DIE:		return new int[]{7,3,0,4,8};
		case Glyphs.DISCOVER:	return new int[]{6,8,10,7};

		case Glyphs.END:		return new int[]{9,0,10,4,6,9};
		case Glyphs.ENLIGHTEN:	return new int[]{1,2,0,1,9,6,8,10};

		case Glyphs.FUTURE:		return new int[]{6,2,4,8};

		case Glyphs.GAIN:		return new int[]{5,3};

		case Glyphs.HUMAN:		return new int[]{1,2,4,10,3,1};

		case Glyphs.IMPROVE:	return new int[]{4,0,2,6};

		case Glyphs.JOURNEY: 	return new int[]{10,7,5,1,0,2,6};

		case Glyphs.MIND:		return new int[]{1,0,10,3,1};	

		case Glyphs.NOT:		return new int[]{1,2,4};
				
		case Glyphs.OPEN:		return new int[]{3,4,10,3};

		case Glyphs.PAST:		return new int[]{5,1,3,7};
		case Glyphs.PORTAL:		return new int[]{5,1,2,6,8,4,3,7,5};
		case Glyphs.PRESENT:	return new int[]{1,3,4,2};


		case Glyphs.SEEK:		return new int[]{0,2,1,3,4};
		case Glyphs.SELF:		return new int[]{7,10,8};
		case Glyphs.SHAPER:		return new int[]{7,3,1,9,2,4,8};
		case Glyphs.SIMPLE:		return new int[]{3,4};
		case Glyphs.SOUL:		return new int[]{0,2,4,10,0};

		case Glyphs.TRUTH:		return new int[]{1,0,4,2,0,3,1};
		
		
		default:
		
			return null;


		}
	}

	//open mind obtain enlightenment


	public enum Glyphs{
		ADVANCE, AGAIN,	ATTACK,
		BREATHE, BODY,
		DESTROY, DIE, DISCOVER,
		END, ENLIGHTEN,
		FUTURE,
		GAIN,
		HUMAN,
		IMPROVE,
		JOURNEY,
		MIND,
		NOT,
		OPEN,
		PAST, PORTAL, PRESENT,
		SEEK, SELF, SHAPER, SIMPLE,	SOUL,
		TRUTH

	};
}




