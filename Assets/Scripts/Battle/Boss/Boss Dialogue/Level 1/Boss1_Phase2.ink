VAR Rational = false
VAR Love = false
VAR Hope = false
VAR Acceptance = false

->Phase2

===Phase2===
Boss: .....
Boss: If you think I would trust you with thes-these simple words, you are gravely mistaken!!
Boss: You might sound like my brother, but I still have my doubts!!
Boss: Give me back my brother!!

    + I'm sorry that I didn't live up to your expectations, but.. I am still the same!
        ->PathA2
        ~Rational=true
    + Stop.. you already knew the whole truth!! Stop being delusional
        ->PathB2
        ~Rational=true
    + Come on now... You have to accept the truth, you are dead!!
        ->PathC2
        ~Hope=true
    + [*Show the remnant*] Remember when I made this for you?
        ->PathD2
        ~Hope=true
        
//==============================================PathA==============================================    
=PathA2
Boss: Things change, yes... But you have the audacity to still lie?!?
    + Come on... I'm not lying!! Listen to me!
        ~Rational=true
    + All I ever want is to have a strong trust between us.... That's really
        ~Hope=true
    + I am your brother, if you think I need to lie to you, then I'm saddened
        ~Love=true
    + The sooner you accept that I am your brother, the easier time you will have
        ~Acceptance=true
- ->checkA2

=checkA2
   {Rational||Hope: 
        Boss: *Sigh* Your persistence is annoying, you are giving me a headache!!
        ~Rational = false
        ~Hope = false
        ->PathA3_1
    -else:
        Boss: Are you implying that I'm being a problematic child?? Are you insulting me now??
        ~Love = false
        ~Acceptance = false
        ->PathA3_2
    }
    
=PathA3_1
    + That is really disappointing to hear.. 
        ~Love=true
    + Then let's end this charade of yours and I guarantee you'll be happier!!
        ~Hope=true
    + Maybe you are a goner... this place has corrupted you...
        ~Acceptance=true
    + If you feel that way, that means I must be on the right track!!
        ~Rational=true
- ->checkA4

=PathA3_2
    + If that is how you feel, then maybe..... this place has already corrupted you!!
        ~Acceptance=true
    + I am... lost... for words..
        ~Love=true
    + Don't let your anger take control of you!! 
        ~Hope=true
    + All of these are your assumptions! Stop jumping to conclusions
        ~Rational=true
    
- ->checkA4

=checkA4
   {Rational||Hope: 
        Boss: Maybe... maybe you are right... 
        Boss: No... NO! I CAN'T BE THE ONE AT FAULT
        ~Rational = false
        ~Hope = false
        ->PathA4_1
    -else:
        Boss: Hah! Your motivations are so FEEBLE!! I already feel better.
        ~Love = false
        ~Acceptance = false
        ->PathA4_2
    }

=PathA4_1
    + It's okay to apologise if you have done something wrong
        ~Rational=true
    + I'm.... I think I'm done trying to convince you any further
        ~Acceptance=true
    + This is one step forward sis, lets resolve this...
        ~Rational=true
    + It will be easier if you can just own up to it!
        ~Love=true
        
- ->checkA5

=PathA4_2
    + You must realize this place is affecting your mind!
        ~Rational=true
    + What do you hope to gain by insulting me?!? I'm trying to help you!
        ~Hope=true
    + So that's it?? You are just going to accept this place as your final stay?
        ~Acceptance=true
    + Is your goal to piss me off? Is that just it?!?
        ~Love=true
    
- ->checkA4

=checkA5
   {Rational||Hope: 
        Boss: My brother would actually know what I want and how to make me happy... THAT IS NOT YOU
        ~Rational = false
        ~Hope = false
        ->PathA5_1
    -else:
        Boss: Thankfully, this will be the last time I'll talk to you. What a waste of time and energy!
        ~Love = false
        ~Acceptance = false
        ->PathA5_2
    }

=PathA5_1
    + Real or not, I still care for you no matter what
        ~Love=true
    + Then you can keep waiting, I want to see how long you are willing wait
        ~Acceptance=true
    + I may come out second best, but this is not over yet. I will convince you!
        ~Rational=true
    + I hope this brother of yours will help save you then
        ~Hope=true

- ->checkA6

=PathA5_2
    + If that's how you feel, there is no point in trying to reach you.
        ~Acceptance=true
    + Disheartening.... that's how I would describe how i feel....
        ~Love=true
    + Why can't you be patient and have a little faith in me??
        ~Hope=true
    + You want to get out of this conversation? Why?
        ~Rational=true

- ->checkA6

=checkA6
   {Rational||Hope: 
        Boss: You will never understand what I've been through..... Pain... Suffering...
        ~Rational = false
        ~Hope = false
        ->PathA6_1
    -else:
        Boss: That's great!! It's about time you give up!! Now... where is my real brother..
        ~Love = false
        ~Acceptance = false
        ->PathA6_2
    }

=PathA6_1
    + I don't know, but I want to... that's what being family is all about
        ~Rational=true
    + Why not give me a chance... calm down and hopefully... You can be at ease
        ~Hope=true
    + Find someone else to talk to you without losing their mind compared to me
        ~Acceptance=true
    + No one else would have have the patience to continue, except me... trust me..
        ~Love=true
- ->checkA7

=PathA6_2
    + You can just rot here then, you are beyond saving
        ~Acceptance=true
    + You should know already know I'm not giving up, that's how your brother operates
        ~Rational=true
    + You will give in and give me a chance, I just know of it!
        ~Hope=true
    + You are truly pushing your luck, playing hard to get... 
        ~Love=true

- ->checkA7

=checkA7
Boss: *She seems to calm down for real time this time...*
    + [Ask what she is thinking about] Do you really trust me now? Are you calm now?
    + [Ask how she feels] Sis? Are you okay?

- ->DONE




=PathB2
What is this?!? Are you putting me on the spot? My real brother would never do that!!


- ->DONE

=PathC2
No


- ->DONE


=PathD2

- ->DONE
->END