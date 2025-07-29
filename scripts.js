// scripts.js
document.addEventListener('DOMContentLoaded', () => {
  // referências à UI
  const titleEl = document.getElementById('activityTitle');
  const descEl = document.getElementById('activityDescription');
  const messagesEl = document.getElementById('messages');
  const durationInput = document.getElementById('durationInput');
  let current = null;

  // dados das atividades
  const activities = {
    breathing: {
      title: 'Breathing Activity',
      description:
        'This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.',
      run: async (duration) => {
        const end = Date.now() + duration * 1000;
        while (Date.now() < end) {
          messagesEl.textContent = 'Breathe in...';
          await showCountDown(4);
          messagesEl.textContent = 'Breathe out...';
          await showCountDown(6);
        }
      }
    },
    reflection: {
      title: 'Reflection Activity',
      description:
        'This activity will help you reflect on times in your life when you have shown strength and resilience.',
      prompts: [
        'Think of a time when you stood up for someone else.',
        'Think of a time when you did something really difficult.',
        'Think of a time when you helped someone in need.',
        'Think of a time when you did something truly selfless.'
      ],
      questions: [
        'Why was this experience meaningful to you?',
        'Have you ever done anything like this before?',
        'How did you get started?',
        'How did you feel when it was complete?',
        'What made this time different than other times when you were not as successful?',
        'What is your favorite thing about this experience?',
        'What could you learn from this experience that applies to other situations?',
        'What did you learn about yourself through this experience?',
        'How can you keep this experience in mind in the future?'
      ],
      run: async (duration) => {
        // prompt inicial
        const prompt = randomItem(activities.reflection.prompts);
        messagesEl.textContent = prompt + '\n\n(Press ENTER when ready)';
        await waitEnter();
        // perguntas com spinner
        const end = Date.now() + duration * 1000;
        let unused = [...activities.reflection.questions];
        while (Date.now() < end) {
          if (unused.length === 0) unused = [...activities.reflection.questions];
          const q = unused.splice(Math.floor(Math.random() * unused.length), 1)[0];
          messagesEl.textContent = q;
          await showSpinner(4);
        }
      }
    },
    listing: {
      title: 'Listing Activity',
      description:
        'This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.',
      prompts: [
        'Who are people that you appreciate?',
        'What are personal strengths of yours?',
        'Who are people that you have helped this week?',
        'When have you felt the Holy Ghost this month?',
        'Who are some of your personal heroes?'
      ],
      run: async (duration) => {
        const prompt = randomItem(activities.listing.prompts);
        messagesEl.textContent = prompt + '\n\nYou may begin in...';
        await showCountDown(5);
        let count = 0;
        const end = Date.now() + duration * 1000;
        while (Date.now() < end) {
          const response = promptUser('> ');
          if (response.trim()) count++;
        }
        messagesEl.textContent = `You listed ${count} items!`;
      }
    }
  };

  // botões
  document.getElementById('breathingBtn').onclick = () => select('breathing');
  document.getElementById('reflectionBtn').onclick = () => select('reflection');
  document.getElementById('listingBtn').onclick = () => select('listing');
  document.getElementById('startBtn').onclick = startActivity;

  function select(key) {
    current = key;
    titleEl.textContent = activities[key].title;
    descEl.textContent = activities[key].description;
    messagesEl.textContent = '';
  }

  async function startActivity() {
    if (!current) {
      alert('Please select an activity first.');
      return;
    }
    const duration = parseInt(durationInput.value, 10);
    if (isNaN(duration) || duration < 1) {
      alert('Enter a valid positive duration.');
      return;
    }
    messagesEl.textContent = 'Get ready...';
    await showSpinner(3);
    await activities[current].run(duration);
    messagesEl.textContent = `Well done!\nYou have completed the ${activities[current].title} for ${duration} seconds.`;
    await showSpinner(3);
  }

  // animações e utilitários
  function showSpinner(seconds) {
    return new Promise(resolve => {
      const seq = ['|', '/', '—', '\\\\'];
      let i = 0;
      const end = Date.now() + seconds * 1000;
      const iv = setInterval(() => {
        messagesEl.textContent = seq[i++ % seq.length];
        if (Date.now() >= end) {
          clearInterval(iv);
          resolve();
        }
      }, 250);
    });
  }

  function showCountDown(seconds) {
    return new Promise(resolve => {
      let i = seconds;
      const iv = setInterval(() => {
        messagesEl.textContent = i--;
        if (i < 0) {
          clearInterval(iv);
          resolve();
        }
      }, 1000);
    });
  }

  function waitEnter() {
    return new Promise(resolve => {
      window.addEventListener('keydown', function onKey(e) {
        if (e.key === 'Enter') {
          window.removeEventListener('keydown', onKey);
          resolve();
        }
      });
    });
  }

  function randomItem(arr) {
    return arr[Math.floor(Math.random() * arr.length)];
  }

  function promptUser(promptText) {
    return window.prompt(promptText) || '';
  }
});
