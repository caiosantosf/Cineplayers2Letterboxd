module.exports = (sequelize, Sequelize) => {
    const Person = sequelize.define("people", {
      name: {type: Sequelize.STRING}
    })

    Person.associate = models => {
      models.people.belongsToMany(models.movies, {
        through: 'moviePeople',
        as: 'movie',
        foreignKey: 'personId',
        otherKey: 'movieId'
      })
    }
  
    return Person
  }