using UnityEngine;
using System.Collections;

public class TalkSettings : MonoBehaviour
{
    public int startTalkNum = 0;

    TalkProcess talk;
    int count = 0;


    void Start()
    {

        talk = GameObject.Find("GameControlObject").GetComponent<TalkProcess>();


    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {

            talk.endTalk();

        }
        if (count == 0) talkSet();
        count++;
    }


    /*会話時に使用する関数表一覧===========================================================================
		

		☆talk.setTalk( int talkNum , float drawTime , int drawFace , string charaName , string talkString );
			会話にセリフをセットします
				
			talkNum		登録する会話の識別番号、playTalkのtalkNumで呼び出したい番号をいれます
			drawTime	そのセリフを描画しておく時間です。秒単位
			drawFace	描画する顔グラの識別番号です。
			charaName	描画したいキャラクターの名前を入れます
			talkString	描画したいセリフを入れます



		☆talk.playTalk( int talkNum , int talkMode )
			会話を再生させます
			一旦playTalkで再生させるとその会話が終わるか、endTalk関数を呼び出すまで
			新しい会話は開始されません。

			talkNum		会話の識別番号を入れます。setTalkのtalkNumで入れた番号の会話が呼び出されます

			talkMode	会話のモードを入れます。
						talkModeが0プレイヤーが自ら会話を飛ばすモード、1がプレイヤーが飛ばさず時間で
						セリフが進むモードです。
						talkModeが0の時はアクションの操作は一切受け付けず、制限時間も止まります。

		☆talk.endTalk()
			現在再生されている会話を終了します。

	=====================================================================================================*/

    void talkSet()
    {

        /*========================================================================
		追加時はこんな感じで、順番に追加されていきます。
		ココは本編集の時は消しといてね（はぁと）
		========================================================================*/
        //■000～099■Tutorial
        
        //■100～199■Stage1
        talk.setTalk(100, 3.0f,  "？？？", "ここは・・・？");
        talk.setTalk(100, 5.0f,  "？？？", "なんで私はここに・・・？");
        talk.setTalk(100, 7.0f,  "？？？", "わからない、家で普通に寝たはずじゃ・・・？");
        talk.setTalk(100, 8.0f,  "？？？", "・・・？あの熊のぬいぐるみ・・・。浮いてる・・・？");
        talk.setTalk(100, 5.0f,  "？？？", "ひぃ！！お、襲ってきた！！");

        talk.setTalk(101, 3.0f, "？？？", "なにか声が聞こえる・・・？");
        talk.setTalk(101, 5.0f, "？？？", "詩・・・？");
        talk.setTalk(101, 7.0f, "少女の声", "わからない、家で普通に寝たはずじゃ・・・？");
        talk.setTalk(101, 8.0f, "少女の声", "それはそれは昔のこと");
        talk.setTalk(101, 5.0f, "少女の声", "海際の街に一人の乙女が住んでいた");
        talk.setTalk(101, 8.0f, "少女の声", "その名は・・・");

        talk.setTalk(102, 8.0f, "？？？", "また・・・何か声が？");
        talk.setTalk(102, 8.0f, "？？？", "（心に直接聞こえてるの・・・？）");
        talk.setTalk(102, 8.0f, "少女の声", "あなたも彼女も同じだった");
        talk.setTalk(102, 8.0f, "少女の声", "海際の街で　あなたたちは遊んでいた");
        talk.setTalk(102, 8.0f, "少女の声", "彼女とあなた");
        talk.setTalk(102, 8.0f, "少女の声", "それは天上の天使たちも");
        talk.setTalk(102, 8.0f, "少女の声", "うらやむような夢だった");

        talk.setTalk(103, 8.0f, "少女の声", "しかし彼女に不幸が訪れた");
        talk.setTalk(103, 8.0f, "少女の声", "海際の街で");
        talk.setTalk(103, 8.0f, "少女の声", "あなたに忘れ去られ殺してしまった");
        talk.setTalk(103, 8.0f, "少女の声", "美しい「アナベル」を");
        talk.setTalk(103, 8.0f, "少女の声", "そこで天上から使者が来て");
        talk.setTalk(103, 8.0f, "少女の声", "彼女を海から取り上げて遺跡の中に閉じ込めてしまった");
        talk.setTalk(103, 8.0f, "少女の声", "深く閉じた遺跡に中に");




        talk.endTalk();

        talk.playTalk(startTalkNum, 0);
    }

}