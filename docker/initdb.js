conn = new Mongo();
db = conn.getDB("blogdoftdb");
db.auth('root', 's4lesc0nsum3r');
db.createUser({ user: 'root', pwd: 's4lesc0nsum3r', roles: [{ role: 'readWrite', db: 'blogdoftdb' }] });
db.createCollection("sales", {
    validator: {
        $jsonSchema: {
            bsonType: "object",
            required: ["id", "customerIdentity", "number", "date", "items"],
            properties: {
                id: {
                    bsonType: "string",
                    description: "'sale id' is required"
                },
                customerIdentity: {
                    bsonType: "string",
                    description: "'customer identity' is required"
                },
                number: {
                    bsonType: "string"
                },
                date: {
                    bsonType: "date"
                },
                items: {
                    bsonType: ["array"],
                    minItems: 1,
                    uniqueItems: true,
                    items: {
                        bsonType: "object",
                        required: ["quantity", "value", "product"],
                        properties: {
                            quantity: {
                                bsonType: "double"
                            },
                            value: {
                                bsonType: "double"
                            },
                            product: {
                                bsonType: "object",
                                required: ["id", "description", "value"],
                                properties: {
                                    id: {
                                        bsonType: "string",
                                        description: "'id' is required"
                                    },
                                    description: {
                                        bsonType: "string",
                                        description: "'description' is required"
                                    },
                                    value: {
                                        bsonType: "double",
                                        description: "'value' is required"
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
});