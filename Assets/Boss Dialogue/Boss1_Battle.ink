VAR Rational = false
VAR Love = false
VAR Hope = false
VAR Acceptance = false
 
 
 ->battle
 
 ===battle===
//==============================================================TURN 1============================================================
You are not the brother I know, give me back my brother!!
    + I am still me?
        No.. no! You are a FAKE! You are a LIAR!!
        ->Path_A
    + Stop acting like you do not know anything!!
        Stop making me uncomfortable, I don't understand!
        ->Path_B2
    + I... just hope you are already at a better place..
        That doesn't make any sense to me..
        ->Path_C2
    + [Mention about something in the past] We used to play by the riverside, it's me! Dont you remember??
        I dont buy it!!
        ->Path_D2
 
 //PATH A
 //==============================================================TURN 2============================================================
 ===Path_A===
    + You should know that I am not lying.. 
        ~ Hope = true
    + Do you think this is easy for me right now??
        ~ Love = true
    + Being honest is how I do things now...
        ~ Rational = true
    + Prove it to me that I lied to you.
        ~ Rational = true

- ->check2

//==============================================================CHECK 2============================================================

=check2
    {Rational || Hope:
        How about you prove to me that you didn't lie!? I know you can't!
        ~Rational = false
        ~Hope = false
        ->Path_A3_1
    - else:
        Yeah!... You probably think this is funny somehow!!
        ~Love = false
        ~Acceptance = false
        ->Path_A3_2
    }

//==============================================================TURN 3============================================================
=Path_A3_1
    + When you left this world... I still felt sorrow..
        ~ Love = true
    + You caught me. 
        ~ Acceptance = true
    + I just hope to carry onwards after your passing.
        ~ Hope = true
    + [*Mention something in the past*] You had a doll that you cherished, satisfied?
        ~ Rational = true
        
- -> check3

=Path_A3_2
    + Alright, you got me.
        ~ Acceptance = true
    + Thats not true at all!
        ~ Hope = true
    +  You are so naive
        ~ Love = true
    +  If I can't be honest with you, then there is something wrong with me.
        ~ Rational = true

- ->check3

//==============================================================CHECK 3============================================================

=check3
    {Rational||Hope:
        Are you really telling the truth? You must be still lying!
        ~Rational = false
        ~Hope = false
        ->PathA4_1
    -else:
        Blah, blah, blah. You are making this unbearable and making me miserable!
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

- ->check4

=PathA4_2
    + I need to you snap out of this right NOW!
        ~ Rational = true
    + How could you not trust me?
        ~ Love = true
    + Okay, FINE. SO BE IT!! 
        ~ Acceptance = true
    + I hope you can come to your senses, please.
        ~ Hope = true

- ->check4

//==============================================================CHECK 4============================================================
=check4
    {Rational||Hope:
        At the end of the day, it's just your side of story. Do you really think your "talk" is going to help you now?
        ~Rational = false
        ~Hope = false
        ->PathA5_1
    -else:
        So now it's MY fault? You ALWAYS want to be right!
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

- ->check5


=PathA5_2
    + If that's how you feel, then there is no point of me talking to you.
        ~ Acceptance = true
    + There you go again, jumping into conclusions.
        ~ Acceptance = true
    + Please... Just believe me on for once!
        ~ Hope = true
    + I didn't say that, and you know it.
        ~ Rational = true

- ->check5

//==============================================================CHECK 5============================================================

=check5
    {Rational||Hope:
        In the end, all you can do is bark. All for NOTHING!
        ~Rational = false
        ~Hope = false
        ->PathA6_1
    -else:
        Stop trying to correct me with your logic and reasoning!
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
        
- ->check6

=PathA6_2
    + You are beyond saving...
        ~ Acceptance = true
    + You can't just for once.... BELIEVE ME??
        ~ Love = true
    + If you think that is the case, as long as you believe me, I'll take it
        ~ Hope = true
    + Provoke me all you want, It is not going to break me
        ~ Rational = true


- ->check6
//==============================================================CHECK 6============================================================
=check6
    {Rational||Hope:
        No matter what you say, It's not going to change my mind!!
        ~Rational = false
        ~Hope = false
        ->PathA7_1
    -else:
        My brother would never leave me behind, YOU AREN'T MY BROTHER
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
- ->check7

=PathA7_2
    + You know what? Only a monster like you would be so stubborn
        ~ Acceptance = true
    + [*Slap her?*]
        ~ Love = true
    + Thats fine if you think I'm a fake, I'm not giving in
        ~ Rational = true
    + .... Please.. Just trust me, this is my last request!!!
        ~ Hope = true
- ->check7

//==============================================================CHECK 7============================================================
=check7
    {Rational||Hope:
        "Your sister seems to have calmed down slightly"
        ~Rational = false
        ~Hope = false
        Phase 2 
        ->DONE
    -else:
        This is goodbye.... I dont know why I wasted my time...
        ~Love = false
        ~Acceptance = false
        Game over
        ->DONE
    }
    
//==========================================================================================================================
===Path_B2===
->DONE


===Path_C2===
->DONE


===Path_D2===
->DONE


->END