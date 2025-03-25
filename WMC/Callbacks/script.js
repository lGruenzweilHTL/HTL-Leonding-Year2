function makeUppercase(value) {
    console.log(value.toUpperCase())
}

function reverseString(value) {
    console.log(value.split('').reverse().join(''))
}

function handleName(name, cb) {
    const fullName = `${name} smith`
    cb(fullName)
}

handleName('peter', makeUppercase)
handleName('peter', reverseString)
handleName('susan', value => console.log(value))

document.addEventListener('DOMContentLoaded', () => {
    const btn = document.querySelector('.btn')
    const first = document.querySelector('.first')
    const second = document.querySelector('.second')
    const third = document.querySelector('.third')
    btn.addEventListener('click', () => {
        console.log('button clicked')
        setTimeout(() => {
            first.style.color = 'red'
            setTimeout(() => {
                second.style.color = 'blue'
                setTimeout(() => {
                    third.style.color = 'green'
                }, 2000)
            }, 3000)
        }, 5000)
    })
})
