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
        ->PathA1
        ~Rational=true
    + Stop.. you already knew the whole truth!! Stop being delusional
        ->PathB1
        ~Rational=true
    + Come on now... You have to accept the truth, you are dead!!
        ->PathC1
        ~Hope=true
    + [*Show the remnant*] Remember when I made this for you?
        ->PathD1
        ~Hope=true
        
//==============================================PathA==============================================    
=PathA1
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
        ->PathA2_1
    -else:
        Boss: Are you implying that I'm being a problematic child?? Are you insulting me now??
        ~Love = false
        ~Acceptance = false
        ->PathA2_2
    }
    
=PathA2_1
    + My 

->DONE
=PathA2_2

->DONE


=PathB1
What is this?!? Are you putting me on the spot? My real brother would never do that!!


- ->DONE

=PathC1
No


- ->DONE
=PathD1

- ->DONE
->END