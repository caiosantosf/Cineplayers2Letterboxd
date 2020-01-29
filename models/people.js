module.exports = (sequelize, Sequelize) => {
    const Person = sequelize.define("people", {
      name: {type: Sequelize.STRING}
    })
  
    return Person
  }