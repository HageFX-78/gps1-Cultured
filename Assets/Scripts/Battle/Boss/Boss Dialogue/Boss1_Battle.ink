VAR Rational = false
VAR Love = false
VAR Hope = false
VAR Acceptance = false
 
 
 ->battle
 
 ===battle===
//==============================================================TURN 1============================================================
Boss: You are not the brother I know, give me back my brother!! #Rational #Rational # Hope #Hope
    + I am still me? 
        ->PathA2
    + Stop acting like you do not know anything!! 
        ->PathB2
    + I... just hope you are already at a better place.. 
        ->PathC2
    + [Mention about something in the past] We used to play by the riverside, it's me! Dont you remember?? 
        ->PathD2
 
 //==============================================================PATH A============================================================
 //==============================================================TURN 2============================================================
 =PathA2
 Boss: No.. no! You are a FAKE! You are a LIAR!! #Hope # Love #Rational #Rational
    + You should know that I am not lying.. 
        ~ Hope = true
    + Do you think this is easy for me right now??
        ~ Love = true
    + Being honest is how I do things now...
        ~ Rational = true
    + Prove it to me that I lied to you.
        ~ Rational = true
- ->checkA2

//==============================================================CHECK 2============================================================

=checkA2
    {Rational || Hope: 
        Boss: How about you prove to me that you didn't lie!? I know you can't! #Love #Acceptance #Hope #Rational
        ~Rational = false
        ~Hope = false
        ->PathA3_1
    - else:
        Boss: Yeah!... You probably think this is funny somehow!! #Acceptance #Hope #Love #Rational
        ~Love = false
        ~Acceptance = false
        ->PathA3_2
    }

//==============================================================TURN 3============================================================
=PathA3_1
    + When you left this world... I still felt sorrow..
        ~ Love = true
    + You caught me. 
        ~ Acceptance = true
    + I just hope to carry onwards after your passing.
        ~ Hope = true
    + [*Mention something in the past*] You had a doll that you cherished, satisfied?
        ~ Rational = true
        
- -> checkA3

=PathA3_2
    + Alright, you got me.
        ~ Acceptance = true
    + Thats not true at all!
        ~ Hope = true
    +  You are so naive
        ~ Love = true
    +  If I can't be honest with you, then there is something wrong with me.
        ~ Rational = true

- ->checkA3

//==============================================================CHECK 3============================================================

=checkA3
    {Rational||Hope:
        Boss: Are you really telling the truth? You must be still lying! #Love #Rational #Acceptance #Hope
        ~Rational = false
        ~Hope = false
        ->PathA4_1
    -else:
        Boss: Blah, blah, blah. You are making this unbearable and making me miserable! #Rational #Love #Acceptance # Hope
        ~Love = false
        ~Acceptance = false
        ->PathA4_2
    }
    
    
//==============================================================TURN 4============================================================
=PathA4_1
    + No, I'm just messing with you
        ~ Love = true
    + Of course! But you already knew that, didn't you...
        ~ Rational = true
    + It is up to you to accept my answer.
        ~ Acceptance = true
    + I am! Hopefully we can just put this behind us
        ~ Hope = true

- ->checkA4

=PathA4_2
    + I need to you snap out of this right NOW!
        ~ Rational = true
    + How could you not trust me?
        ~ Love = true
    + Okay, FINE. SO BE IT!! 
        ~ Acceptance = true
    + I hope you can come to your senses, please.
        ~ Hope = true

- ->checkA4

//==============================================================CHECK 4============================================================
=checkA4
    {Rational||Hope:
        Boss: At the end of the day, it's just your side of story. Do you really think your "talk" is going to help you now?
        #Rational #Hope #Acceptance #Love
        ~Rational = false
        ~Hope = false
        ->PathA5_1
    -else:
        Boss: So now it's MY fault? You ALWAYS want to be right! #Acceptance #Acceptance #Hope #Rational
        ~Love = false
        ~Acceptance = false
        ->PathA5_2
   
    }
    
    
//==============================================================TURN 5============================================================
=PathA5_1
    + Then I'll do WHATEVER it takes for you to believe me.
        ~ Rational = true
    + STOP being in DENIAL!! Just listen to me for once!!
        ~ Hope = true
    + Fine then... I guess there is no point in trying to help you.
        ~ Acceptance = true
    + You are just saying that to hurt me...
        ~ Love = true

- ->checkA5


=PathA5_2
    + If that's how you feel, then there is no point of me talking to you.
        ~ Acceptance = true
    + There you go again, jumping into conclusions.
        ~ Acceptance = true
    + Please... Just believe me on for once!
        ~ Hope = true
    + I didn't say that, and you know it.
        ~ Rational = true

- ->checkA5

//==============================================================CHECK 5============================================================

=checkA5
    {Rational||Hope:
        Boss: In the end, all you can do is bark. All for NOTHING! #Acceptance #Love #Hope #Rational
        ~Rational = false
        ~Hope = false
        ->PathA6_1
    -else:
        Boss: Stop trying to correct me with your logic and reasoning! #Acceptance #Love #Hope #Rational
        ~Love = false
        ~Acceptance = false
        ->PathA6_2
    }

//==============================================================TURN 6============================================================
=PathA6_1
    + You are truly goner
        ~ Acceptance = true
    + Why are you making things difficult for me!!
        ~ Love = true
    + As long as you trust me, I don't care what you think about make
        ~ Hope = true
    + It doesn't matter if you think I can't back it up, I'm trying to save you!
        ~ Rational = true
        
- ->checkA6

=PathA6_2
    + You are beyond saving...
        ~ Acceptance = true
    + You can't just for once.... BELIEVE ME??
        ~ Love = true
    + If you think that is the case, as long as you believe me, I'll take it
        ~ Hope = true
    + Provoke me all you want, It is not going to break me
        ~ Rational = true


- ->checkA6
//==============================================================CHECK 6============================================================
=checkA6
    {Rational||Hope:
        Boss: No matter what you say, It's not going to change my mind!! #Acceptance #Rational #Acceptance #Hope
        ~Rational = false
        ~Hope = false
        ->PathA7_1
    -else:
        Boss: My brother would never leave me behind, YOU AREN'T MY BROTHER #Acceptance #Love #Rational #Hope
        ~Love = false
        ~Acceptance = false
        ->PathA7_2
    }
//==============================================================TURN 7============================================================
=PathA7_1
    + Fine, you know what? You are just a monster
        ~ Acceptance = true
    + That will not shake me, I know that I'm RIGHT!
        ~ Rational = true
    + [Give Up?]I'm leaving and I'm DONE TRYING
        ~ Acceptance = true
    + [*Kneel Down*] P- Please...
        ~Hope = true
- ->checkA7

=PathA7_2
    + You know what? Only a monster like you would be so stubborn
        ~ Acceptance = true
    + [*Slap her*]
        ~ Love = true
    + Thats fine if you think I'm a fake, I'm not giving in
        ~ Rational = true
    + .... Please.. Just trust me, this is my last request!!!
        ~ Hope = true
- ->checkA7

//==============================================================CHECK 7============================================================
=checkA7 //have to fix in code
    "Your sister seems to have calmed down slightly"
    Phase 2 
- ->PathA8

//==============================================================TURN 8============================================================
=PathA8
    + Do you believe that I am your brother now?
        ...
    + I would never dare pretend to be someone you know
        Boss: If you think I would trust you with these simple words... You are gravely mistaken!!
- ->DONE
    
    
//==============================================================PATH B============================================================
//==============================================================TURN 2============================================================
=PathB2
Boss: Stop making me uncomfortable, I don't understand! #Rational #Rational #Love #Hope
    + You are in denial right now..
        ~ Rational = true
    + That's because you are RUNNING away from reality!
        ~ Rational = true
    + What you are doing is going to make it more miserable!!
        ~ Love = true
    + I pray that you will trust me what im about to say..
        ~ Hope = true
- ->checkB2

//==============================================================CHECK 2============================================================

=checkB2
    {Rational||Hope:
        Boss: WHO GIVES YOU THE RIGHT TO LECTURE ME!! #Love #Acceptance #Hope #Rational
        ~Rational = false
        ~Hope = false
        ->PathB3_1
    -else:
        Boss: Hah! Funny how YOU are the person telling ME this. #Rational #Acceptance #Love #Hope
        ~Love = false
        ~Acceptance = false
        ->PathB3_2
    }
//==============================================================TURN 3============================================================
=PathB3_1
    + I will fail as a brother if I don't confront you
        ~ Love = true
    + Do you think I'll accept this attitude of yours?
        ~ Acceptance = true
    + Stop being SO defensive and LISTEN to ME for ONCE!!
        ~ Hope = true
    + With the way you are acting right now... I have every right...
        ~ Rational = true
- -> checkB3

=PathB3_2
    + Then we should both face reality together..
        ~ Rational = true
    + All this is fake, YOU are not REAL!
        ~ Acceptance = true
    + You know nothing of me...
        ~ Love = true
    + Then your wish right now.. It should be leaving this place
        ~ Hope = true
- ->checkB3

//==============================================================CHECK 3============================================================
=checkB3
    {Rational||Hope:
        Boss: I'm trying to!! But YOU are the one being unreasonable and annoying!! #Rational #Hope #Acceptance #Acceptance
        ~Rational = false
        ~Hope = false
        ->PathB4_1
    -else:
        Boss: You are trying to bully me! My brother would never do this to me! HMPH! #Love #Hope #Acceptance #Rational
        ~Love = false
        ~Acceptance = false
        ->PathB4_2
    }
//==============================================================TURN 4============================================================
=PathB4_1
    + Don't try to twist the story to your favor!
        ~Rational = true
    + I hope you can cooperate right know
        ~Hope=true
    + I'm really disappointed on how you reacted
        ~Acceptance=true
    + Do you really expect me to accept your tantrum??
        ~Acceptance=true
- ->checkB4

=PathB4_2
    + The nerve of you to say that!!
        ~Love=true
    + Then I hope you can drop this tantrum and leave it here.
        ~Hope=true
    + And how am I supposed to react to that? Accept it?
        ~Acceptance=true
    + Do you reeally think taunting me will affect me?
        ~Rational=true
- ->checkB4

//==============================================================CHECK 4============================================================
=checkB4
    {Rational||Hope:
        Boss: ARGHH!! You don't understand... It is YOU who keeps adding salt to my wound!!! #Acceptance #Rational #Hope #Acceptance
        ~Rational = false
        ~Hope = false
        ->PathB5_1
    -else:
        Boss: You KNOW what I said is the truth... #Acceptance #Hope #Love #Rational
        ~Love = false
        ~Acceptance = false
        ->PathB5_2
    }

//==============================================================TURN 5============================================================
=PathB5_1
    + Nowww we are playing the blame game. 
        ~Acceptance=true
    + You just need to relax and take a deep breath right now.
        ~Rational=true
    + Just hoped you would see my point here..
        ~Hope=true
    + Well, no point talking to you if you keep acting this way.
        ~Acceptance=true
- ->checkB5

=PathB5_2
    + You need to stop this attitude of yours.. OR ELSE...
        ~Acceptance=true
    + And I wish for you to be in a better place instead of throwing a tantrum.
        ~Hope=true
    + What are you trying to prove here!? That I'm wrong??
        ~Love=true
    + I know.... Thats why I'm trying to be better.
        ~Rational=true
- ->checkB5
//==============================================================CHECK 5============================================================
=checkB5
    {Rational||Hope:
        Boss: The audacity of you to say that really impressed me, but I hardly feel anything #Acceptance #Love #Rational #Hope
        ~Rational = false
        ~Hope = false
        ->PathB6_1
    -else:
        Boss: Looking at you panic... IS THE BEST THING I'VE SEEN IN AWHILE!! HAHAHA #Acceptance #Love #Rational #Hope
        ~Love = false
        ~Acceptance = false
        ->PathB6_2
    }
//==============================================================TURN 6============================================================
=PathB6_1
    + Am i suppose to give in to your attitude!??
        ~Acceptance=true
    + Are you looking down on me now??
        ~Love=true
    + I guess I'll keep trying to get to you!!!
        ~Rational=true
    + Eventually you will give in... I just need more time..
        ~Hope=true
- ->checkB6

=PathB6_2
    + Yes. And I'm done TRYING
        ~Acceptance=true
    + It is all your fault!!
        ~Love=true
    + Do you really think this charade will make me give in??
        ~Rational=true
    + Panicking just shows that I still care for you
        ~Hope=true
- ->checkB6

//==============================================================CHECK 6============================================================
=checkB6
    {Rational||Hope:
        Boss: Still trying?!? At the end of the day, you are just weak!! #Acceptance #Love #Rational #Hope
        ~Rational = false
        ~Hope = false
        ->PathB7_1
    -else:
        Boss: I'm already better... without you. So run along now!! #Acceptance #Love #Rational #Hope
        ~Love = false
        ~Acceptance = false
        ->PathB7_2
    }
//==============================================================TURN 7============================================================
=PathB7_1
    + I will not give in to your little act!!
        ~Acceptance=true
    +Are you still looking down on me??
        ~Love=true
    + I won't stop till I save you!
        ~Rational=true
    + Eventually you will believe me, I just need... more.. time
        ~Hope=true
- ->checkB7

=PathB7_2
    + Then I'm done talking to you!!
        ~Acceptance=true
    + Then I have nothing but disappointment in you!!
        ~Love=true
    + I'm not leaving until you trust me!!!
        ~Rational=true
    + As long as I'm still breathing, I will not give up!!
        ~Hope=true

- ->checkB7

//==============================================================CHECK 7============================================================
=checkB7 //have to fix in code
    "Your sister seems to have calmed down slightly"
    Phase 2 
- ->PathB8
    
//==============================================================PATH 8============================================================
=PathB8
    + Do you believe that I am your brother now?
        ...
    + I would never dare pretend to be someone you know
        Boss: If you think I would trust you with these simple words... You are gravely mistaken!!
- ->DONE

//==============================================================PATH C============================================================
//==============================================================TURN 2============================================================
=PathC2
Boss: That doesn't make any sense to me.. #Love #Hope $Acceptance #Rational
    + Even if you passed, I will always love you
        ~Love=true
    + Please don't run away from this
        ~Hope=true
    + I already accepted that you are not here
        ~Acceptance=true
    + You need to face the truth
        ~Rational=true
- ->checkC2

//==============================================================CHECK 2============================================================
=checkC2
    {Rational||Hope: 
        Boss: So you are saying I'm running away? Like you!? You are a coward!!? #Hope #Love #Rational #Acceptance
        ~Rational = false
        ~Hope = false
        ->PathC3_1
    -else:
        Boss: All you want is to see me suffer!! #Love #Hope #Acceptance #Rational
        ~Love = false
        ~Acceptance = false
        ->PathC3_2
    }
//==============================================================TURN 3============================================================
=PathC3_1
    + Please... Stop being so in denial!!
        ~Hope=true
    + Then be better than me and face the truth!
        ~Love=true
    + The truth is always hard to swallow!!
        ~Rational=true
    + Then prove to me that you are not!!
        ~Acceptance=true
        
- ->checkC3

=PathC3_2
    + Hear you saying that... It makes me sad..
        ~Love=true
    + Please... You must not think like that
        ~Hope=true
    + Of course... You are always trying to be dramatic..
        ~Acceptance=true
    + The suffering will end if you accept the truth.
        ~Rational=true
- ->checkC3

//==============================================================CHECK 3============================================================
=checkC3
    {Rational||Hope: 
        Boss: YOU think YOU know EVERYTHING... but you don't! #Hope #Rational #Acceptance #Love
        ~Rational = false
        ~Hope = false
        ->PathC4_1
    -else:
        Boss: "Yawn" Keep up with your fake identity.. It won't work! #Love #Hope #Rational #Acceptance
        ~Love = false
        ~Acceptance = false
        ->PathC4_2
    }
//==============================================================TURN 4============================================================
=PathC4_1
    + Just hoping I can talk some sense into you
        ~Hope=true
    + Then prove that you are better than this
        ~Rational=true
    + Then maybe you are not the person I once knew
        ~Acceptance=true
    + You are testing my patience here
        ~Love=true
- ->checkC4

=PathC4_2
    + The way you are acting now is concerning
        ~Love=true
    + Please trust me on this
        ~Hope=true
    + Then I'll just keep on trying to convince you
        ~Rational=true
    + Am I supposed to accept you insulting me?
        ~Acceptance=true
- ->checkC4

//==============================================================CHECK 4============================================================
=checkC4
    {Rational||Hope: 
        Boss: To accept that I'm dead?? Thats nothing but a lie!! #Rational #Hope #Acceptance #Love
        ~Rational = false
        ~Hope = false
        ->PathC5_1
    -else:
        Boss: It's my fault now?!?? All you can do is blame me, but the truth both our suffering is your doing!! #Rational #Love #Hope # Acceptance
        ~Love = false
        ~Acceptance = false
        ->PathC5_2
    }


//==============================================================TURN 5============================================================
=PathC5_1
    + Staying here is not helping either of us!!
        ~Rational=true
    + Please... Open your eyes!
        ~Hope=true
    + So you rather accept this "reality" of yours?
        ~Acceptance=true
    + I'm really disappointed in you
        ~Love=true
- ->checkC5

=PathC5_2
    + We can change only the future, not the past..
        ~Rational=true
    + If you see it that way, then.. I'm sad..
        ~Love=true
    + Eventually, your suffering will be over!
        ~Hope=true
    + Fine then, I'm done trying
        ~Acceptance=true
- ->checkC5

//==============================================================CHECK 5=====================================================
=checkC5
    {Rational||Hope: 
        Boss: I'm suppose to trust you while you continue to lie to my face!?? You DISGUST ME #Rational #Hope #Acceptance #Love
        ~Rational = false
        ~Hope = false
        ->PathC6_1
    -else: 
        Boss: You are weak!!! I'm not surprised #Rational #Acceptance #Love #Hope
        ~Love = false
        ~Acceptance = false
        ->PathC6_2
    }

//==============================================================TURN 6============================================================
=PathC6_1
    + I can prove to you that I didn't lie!!
        ~Rational=true
    + Then I'll keep trying! Until you believe me!!
        ~Hope=true
    + Alright then, I'm done trying to convince you!!
        ~Acceptance=true
    + I'm really disappointed in you....
        ~Love=true
- ->checkC6

=PathC6_2
    + I'm not giving up to you
        ~Rational=true
    + You need to accept that we are not the same!!
        ~Acceptance=true
    + I'm losing my sanity right know!!
        ~Love=true
    +I guarantee you, if you accept the truth, I won't be here anymore
        ~Hope=true
- ->checkC6

//==============================================================CHECK 6=====================================================
=checkC6
    {Rational||Hope: 
        Boss: Not sincere enough!! I hardly feel anything!! #Hope #Rational #Acceptance #Love
        ~Rational = false
        ~Hope = false
        ->PathC7_1
    -else:
        Boss: I'm delighted that this is happening. Seeing you SUFFER make me happy! #Hope #Rational #Acceptance #Love
        ~Love = false
        ~Acceptance = false
        ->PathC7_2
    }


//==============================================================TURN 7============================================================
=PathC7_1
    + ["Kneel Down"] Please... I don't want you to suffer!!
        ~Hope=true
    + I'll keep convincing you until my last breath!!
        ~Rational=true
    + I am DONE TRYING to talk to you!!
        ~Acceptance=true
    + I'm really disappointed in you
        ~Love=true
- ->checkC7

=PathC7_2
    + If that's what it takes to make you happy!! I'll do it!!
        ~Hope=true
    + I'll do everything to make sure you trust me!!
        ~Rational=true
    + I'm DONE TRYING TO HELP YOU!!
        ~Acceptance=true
    + I'm disappointed in you
        ~Love=true
- ->checkC7

//==============================================================CHECK 7=====================================================
=checkC7 //fix in code
    "Your sister seems to have calmed down slightly"
    Phase 2 
- ->PathC8
//==============================================================TURN 8============================================================
=PathC8
    + Do you believe that I am your brother now?
        ...
    + I would never dare pretend to be someone you know
        Boss: If you think I would trust you with these simple words... You are gravely mistaken!!
- ->DONE


//==============================================================PATH D============================================================
//==============================================================TURN 2============================================================
=PathD2
Boss: I dont buy it!! #Rational #Love #Acceptance #Hope
    + I'm not lying, thats my final answer
        ~Rational=true
    + You are making depressed by questioning me...
        ~Love=true
    + Why would you immediately reject that!
        ~Acceptance=true
    + You know that it's not a lie
        ~Hope=true
- ->checkD2

//==============================================================CHECK 2============================================================
=checkD2
    {Rational||Hope: 
        Boss: Get real!! Everyone Lies!! #Rational #Love #Hope #Acceptance
        ~Rational = false
        ~Hope = false
        ->PathD3_1
    -else:
        Boss: I won't change my mind now!! #Hope #Rational #Love #Acceptance
        ~Love = false
        ~Acceptance = false
        ->PathD3_2
    }
//==============================================================TURN 3============================================================
=PathD3_1
    + That depends on the situation..
        ~Rational=true
    + I'm really sad at your reaction..
        ~Love=true
    + Please trust me this one time
        ~Hope=true
    + It doesn't look like you care anyways!!
        ~Acceptance=true
- ->checkD3

=PathD3_2
    + You know I'm telling the truth
        ~Hope=true
    + Do you think I'll just give up now?
        ~Rational=true
    + You are so stubborn!! The truth is all I have for you!!
        ~Love=true
    + It doesn't look like you care anyways!!
        ~Acceptance=true
- ->checkD3

//==============================================================CHECK 3============================================================
=checkD3
    {Rational||Hope: 
        Boss: You fake!! You are trying to bait me into this!! This must be a trap!! #Acceptance #Rational #Love #Hope
        ~Rational = false
        ~Hope = false
        ->PathD4_1
    -else:
        Boss: You are not even trying!! A fake cracks easily!! #Hope #Acceptance #Rational #Love
        ~Love = false
        ~Acceptance = false
        ->PathD4_2
    }

//==============================================================TURN 4============================================================
=PathD4_1
    + I guess I'm done trying to talk to you
        ~Acceptance=true
    + You are better than this, stop overthinking it
        ~Rational=true
    + I'm really disappointed in you right now
        ~Love=true
    + Please stop being self-centered and listen!!
        ~Hope=true
- ->checkD4

=PathD4_2
    + I can be better than this, and so can you.
        ~Hope=true
    + I guess I'm done trying to talk to you.
        ~Acceptance=true
    + You need to be careful with your words.
        ~Rational=true
    + Are you taunting me now!?
        ~Love=true
- ->checkD4

//==============================================================CHECK 4============================================================
=checkD4
    {Rational||Hope: 
        Boss: You are so pathetic with your attempts right now #Hope #Rational #Acceptance #Love
        ~Rational = false
        ~Hope = false
        ->PathD5_1
    -else:
        Boss: So easily to crumble under pressure! So predictable...#Rational #Hope #Love #Acceptance
        ~Love = false
        ~Acceptance = false
        ->PathD5_2
    }

//==============================================================TURN 5============================================================
=PathD5_1
    + There has to be some way for you to trust me
        ~Hope=true
    + Drop the act!! You and I are not so different!!
        ~Rational=true
    + Fine then! I'm done!!
        ~Acceptance=true
    + You are starting to get on my nerves now!!
        ~Love=true
- ->checkD5

=PathD5_2
    + I just want you to be better than this, that's all 
        ~Rational=true
    + I wish you can drop the act now
        ~Hope=true
    + .... How can you say that?? I'm baffled
        ~Love=true
    + I have no choice but to agree with you..
        ~Acceptance=true
- ->checkD5
//==============================================================CHECK 5============================================================
=checkD5
    {Rational||Hope: 
        Boss: Why should i trust you? You are not even real!! #Rational #Hope #Love #Acceptance
        ~Rational = false
        ~Hope = false
        ->PathD6_1
    -else:
        Boss: Watching you STRUGGLE and SUFFER is my cup of tea #Hope #Rational #Acceptance #Love
        ~Love = false
        ~Acceptance = false
        ->PathD6_2
    }

//==============================================================TURN 6============================================================
=PathD6_1
    + Because I'm your brother!
        ~Rational=true
    + Because anywhere is better than here, I can help you!!
        ~Hope=true
    + Because you are starting to irritate me
        ~Love=true
    + Because if you don't, I might not be able to save you
        ~Acceptance=true
- ->checkD6

=PathD6_2
    + At least you will be happy by the end of this
        ~Hope=true
    + I'm not bothered by what you said
        ~Rational=true
    + Maybe you are truly a goner
        ~Acceptance=true
    + You are starting to get on my nerves now
        ~Love=true
- ->checkD6

//==============================================================CHECK 6============================================================
=checkD6
    {Rational||Hope:  
        Boss: At the end of the day, all of this is just an act! #Hope #Rational #Acceptance #Love
        ~Rational = false
        ~Hope = false
        ->PathD7_1
    -else: 
        Boss: You are so pathetic, are you sure you are not fake? #Hope #Rational #Love #Acceptance
        ~Love = false
        ~Acceptance = false
        ->PathD7_2
    }

//==============================================================TURN 7============================================================
=PathD7_1
    + Please.. I hope you can come to your senses for once and listen!!
        ~Hope=true
    + Even so, as long as I can bring you to your sense, I'll perform any act
        ~Rational=true
    + I'm done trying to be nice, I'M DONE!!
        ~Acceptance=true
    + Then you are just an illusion haunting my dream
        ~Love=true
- ->checkD7

=PathD7_2
    + If insulting me gives you happiness, then fine!!
        ~Hope=true
    + Maybe I am, but I'm not giving up on you now!
        ~Rational=true
    + All of this suffering is all your own fault.
        ~Love=true
    + I'm just lost for words...
        ~Acceptance=true
- ->checkD7


//==============================================================CHECK 7============================================================
=checkD7//fix in code
    "Your sister seems to have calmed down slightly"
    Phase 2 
- ->PathD8
//==============================================================TURN 8============================================================
=PathD8
    + Do you believe that I am your brother now?
        ...
    + I would never dare pretend to be someone you know
        Boss: If you think I would trust you with these simple words... You are gravely mistaken!!
- ->DONE

->END